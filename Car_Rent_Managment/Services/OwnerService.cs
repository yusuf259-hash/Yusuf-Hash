using Car_Rent_Managment.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Car_Rent_Managment.Services
{
    public class OwnerService
    {
        public DataTable GetOwnerSummary(int ownerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        (SELECT COUNT(*) FROM Cars WHERE OwnerId = @OwnerId) AS TotalCars,
                        (SELECT COUNT(*) FROM Cars WHERE OwnerId = @OwnerId AND Status = 'Available') AS AvailableCars,
                        (SELECT COUNT(*) FROM Cars WHERE OwnerId = @OwnerId AND Status = 'Rented') AS RentedCars,
                        (SELECT COUNT(*) FROM Cars WHERE OwnerId = @OwnerId AND Status IN ('Maintenance', 'Unavailable')) AS InactiveCars,
                        (
                            SELECT COUNT(*)
                            FROM Bookings b
                            INNER JOIN Cars c ON b.CarId = c.CarId
                            WHERE c.OwnerId = @OwnerId
                        ) AS TotalBookings,
                        (
                            SELECT ISNULL(SUM(b.PayableAmount), 0)
                            FROM Bookings b
                            INNER JOIN Cars c ON b.CarId = c.CarId
                            WHERE c.OwnerId = @OwnerId
                            AND b.PaymentStatus = 'Paid'
                            AND b.BookingStatus <> 'Cancelled'
                        ) AS TotalPaidEarnings,
                        (
                            SELECT ISNULL(SUM(b.PayableAmount), 0)
                            FROM Bookings b
                            INNER JOIN Cars c ON b.CarId = c.CarId
                            WHERE c.OwnerId = @OwnerId
                            AND b.PaymentStatus = 'Unpaid'
                            AND b.BookingStatus = 'Active'
                        ) AS PendingUnpaidAmount,
                        (
                            SELECT COUNT(*)
                            FROM Offers
                            WHERE CreatedByUserId = @OwnerId
                            AND OfferScope IN ('Owner', 'Car')
                            AND Status = 'Active'
                            AND StartDate <= CAST(GETDATE() AS DATE)
                            AND EndDate >= CAST(GETDATE() AS DATE)
                        ) AS ActiveOffers";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OwnerId", ownerId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public DataTable GetOwnerRecentBookings(int ownerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT TOP 10
                        b.BookingId,
                        c.CarName,
                        c.CarNumber,
                        customer.FullName AS CustomerName,
                        customer.Phone AS CustomerPhone,
                        b.RentDate,
                        b.ExpectedReturnDate,
                        b.ActualReturnDate,
                        b.PayableAmount,
                        b.PaymentStatus,
                        b.BookingStatus
                    FROM Bookings b
                    INNER JOIN Cars c ON b.CarId = c.CarId
                    INNER JOIN Users customer ON b.CustomerId = customer.UserId
                    WHERE c.OwnerId = @OwnerId
                    ORDER BY b.BookingId DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OwnerId", ownerId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public DataTable GetOwnerEarningDetails(int ownerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        b.BookingId,
                        c.CarName,
                        c.CarNumber,
                        customer.FullName AS CustomerName,
                        b.RentDate,
                        b.ExpectedReturnDate,
                        b.PayableAmount,
                        b.PaymentStatus,
                        b.BookingStatus,
                        ISNULL(paymentInfo.PaymentMethod, '-') AS PaymentMethod,
                        paymentInfo.PaymentDate
                    FROM Bookings b
                    INNER JOIN Cars c ON b.CarId = c.CarId
                    INNER JOIN Users customer ON b.CustomerId = customer.UserId
                    OUTER APPLY
                    (
                        SELECT TOP 1
                            p.PaymentMethod,
                            p.PaymentDate
                        FROM Payments p
                        WHERE p.BookingId = b.BookingId
                        AND p.CustomerId = b.CustomerId
                        AND p.Amount = b.PayableAmount
                        ORDER BY p.PaymentDate DESC
                    ) paymentInfo
                    WHERE c.OwnerId = @OwnerId
                    AND b.BookingStatus <> 'Cancelled'
                    ORDER BY b.BookingId DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OwnerId", ownerId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public DataTable GetOwnerCarInventoryDetails(int ownerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        c.CarId,
                        c.CarName,
                        c.Brand,
                        c.Model,
                        c.CarNumber,
                        c.Seats,
                        c.PricePerDay,
                        c.Location,
                        c.Status,
                        COUNT(b.BookingId) AS TotalBookings,
                        ISNULL(SUM(CASE WHEN b.PaymentStatus = 'Paid' AND b.BookingStatus <> 'Cancelled' THEN b.PayableAmount ELSE 0 END), 0) AS PaidEarnings
                    FROM Cars c
                    LEFT JOIN Bookings b ON c.CarId = b.CarId
                    WHERE c.OwnerId = @OwnerId
                    GROUP BY
                        c.CarId,
                        c.CarName,
                        c.Brand,
                        c.Model,
                        c.CarNumber,
                        c.Seats,
                        c.PricePerDay,
                        c.Location,
                        c.Status
                    ORDER BY c.CarId DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OwnerId", ownerId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }
    }
}
