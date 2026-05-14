using Car_Rent_Managment.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Car_Rent_Managment.Services
{
    public class BookingService
    {
        private const decimal FinePerDay = 500m;

        public DataTable GetAvailableCars()
        {
            return GetAvailableCarsFiltered("", 0, 0, 0);
        }

        public DataTable GetAvailableCarsFiltered(string location, int minSeats, decimal minPrice, decimal maxPrice)
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
                        c.Description,
                        u.FullName AS OwnerName,
                        ISNULL(bestOffer.OfferId, 0) AS OfferId,
                        ISNULL(bestOffer.OfferTitle, 'No Offer') AS OfferTitle,
                        ISNULL(bestOffer.DiscountPercent, 0) AS DiscountPercent,
                        ISNULL(bestOffer.OfferScope, '-') AS OfferScope
                    FROM Cars c
                    INNER JOIN Users u ON c.OwnerId = u.UserId
                    OUTER APPLY
                    (
                        SELECT TOP 1
                            o.OfferId,
                            o.OfferTitle,
                            o.DiscountPercent,
                            o.OfferScope
                        FROM Offers o
                        WHERE o.Status = 'Active'
                        AND CAST(GETDATE() AS DATE) BETWEEN o.StartDate AND o.EndDate
                        AND
                        (
                            o.CarId = c.CarId
                            OR
                            (
                                o.OfferScope = 'Platform'
                                AND o.CarId IS NULL
                            )
                            OR
                            (
                                o.OfferScope = 'Owner'
                                AND o.CreatedByUserId = c.OwnerId
                                AND o.CarId IS NULL
                            )
                        )
                        ORDER BY o.DiscountPercent DESC
                    ) bestOffer
                    WHERE c.Status = 'Available'
                    AND (@Location = '' OR c.Location LIKE '%' + @Location + '%')
                    AND (@MinSeats = 0 OR c.Seats >= @MinSeats)
                    AND (@MinPrice = 0 OR c.PricePerDay >= @MinPrice)
                    AND (@MaxPrice = 0 OR c.PricePerDay <= @MaxPrice)
                    ORDER BY c.CarId DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Location", location);
                    command.Parameters.AddWithValue("@MinSeats", minSeats);
                    command.Parameters.AddWithValue("@MinPrice", minPrice);
                    command.Parameters.AddWithValue("@MaxPrice", maxPrice);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public bool RentCar(
            int customerId,
            int carId,
            DateTime rentDate,
            DateTime expectedReturnDate,
            decimal totalAmount,
            out string message)
        {
            return RentCar(
                customerId,
                carId,
                rentDate,
                expectedReturnDate,
                totalAmount,
                0,
                totalAmount,
                out message
            );
        }

        public bool RentCar(
            int customerId,
            int carId,
            DateTime rentDate,
            DateTime expectedReturnDate,
            decimal totalAmount,
            decimal discountAmount,
            decimal payableAmount,
            out string message)
        {
            message = "";

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string checkCarQuery = @"
                        SELECT COUNT(*)
                        FROM Cars
                        WHERE CarId = @CarId AND Status = 'Available'";

                    using (SqlCommand checkCommand = new SqlCommand(checkCarQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@CarId", carId);

                        int availableCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (availableCount == 0)
                        {
                            transaction.Rollback();
                            message = "This car is no longer available.";
                            return false;
                        }
                    }

                    string insertBookingQuery = @"
                        INSERT INTO Bookings
                        (
                            CustomerId,
                            CarId,
                            RentDate,
                            ExpectedReturnDate,
                            TotalAmount,
                            DiscountAmount,
                            PayableAmount,
                            PaymentStatus,
                            BookingStatus
                        )
                        VALUES
                        (
                            @CustomerId,
                            @CarId,
                            @RentDate,
                            @ExpectedReturnDate,
                            @TotalAmount,
                            @DiscountAmount,
                            @PayableAmount,
                            'Unpaid',
                            'Active'
                        )";

                    using (SqlCommand bookingCommand = new SqlCommand(insertBookingQuery, connection, transaction))
                    {
                        bookingCommand.Parameters.AddWithValue("@CustomerId", customerId);
                        bookingCommand.Parameters.AddWithValue("@CarId", carId);
                        bookingCommand.Parameters.AddWithValue("@RentDate", rentDate.Date);
                        bookingCommand.Parameters.AddWithValue("@ExpectedReturnDate", expectedReturnDate.Date);
                        bookingCommand.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        bookingCommand.Parameters.AddWithValue("@DiscountAmount", discountAmount);
                        bookingCommand.Parameters.AddWithValue("@PayableAmount", payableAmount);

                        bookingCommand.ExecuteNonQuery();
                    }

                    string updateCarQuery = @"
                        UPDATE Cars
                        SET Status = 'Rented'
                        WHERE CarId = @CarId";

                    using (SqlCommand updateCommand = new SqlCommand(updateCarQuery, connection, transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@CarId", carId);
                        updateCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    if (discountAmount > 0)
                    {
                        message = "Car rented successfully. Discount applied: " + discountAmount.ToString("0.00") + " BDT. Please complete payment.";
                    }
                    else
                    {
                        message = "Car rented successfully. Please complete payment.";
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    message = "Booking failed: " + ex.Message;
                    return false;
                }
            }
        }

        public DataTable GetActiveBookingsByCustomer(int customerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        b.BookingId,
                        b.CarId,
                        c.CarName,
                        c.Brand,
                        c.Model,
                        c.CarNumber,
                        b.RentDate,
                        b.ExpectedReturnDate,
                        b.TotalAmount,
                        b.DiscountAmount,
                        b.PayableAmount,
                        b.PaymentStatus
                    FROM Bookings b
                    INNER JOIN Cars c ON b.CarId = c.CarId
                    WHERE b.CustomerId = @CustomerId
                    AND b.BookingStatus = 'Active'
                    ORDER BY b.BookingId DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public bool ReturnCar(
            int customerId,
            int bookingId,
            DateTime actualReturnDate,
            out int lateDays,
            out decimal fineAmount,
            out string message)
        {
            lateDays = 0;
            fineAmount = 0;
            message = "";

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    int carId = 0;
                    DateTime rentDate = DateTime.MinValue;
                    DateTime expectedReturnDate = DateTime.MinValue;

                    string getBookingQuery = @"
                        SELECT BookingId, CarId, RentDate, ExpectedReturnDate
                        FROM Bookings
                        WHERE BookingId = @BookingId
                        AND CustomerId = @CustomerId
                        AND BookingStatus = 'Active'";

                    using (SqlCommand command = new SqlCommand(getBookingQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@BookingId", bookingId);
                        command.Parameters.AddWithValue("@CustomerId", customerId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                reader.Close();
                                transaction.Rollback();
                                message = "Active booking not found.";
                                return false;
                            }

                            carId = Convert.ToInt32(reader["CarId"]);
                            rentDate = Convert.ToDateTime(reader["RentDate"]);
                            expectedReturnDate = Convert.ToDateTime(reader["ExpectedReturnDate"]);
                        }
                    }

                    if (actualReturnDate.Date < rentDate.Date)
                    {
                        transaction.Rollback();
                        message = "Actual return date cannot be before rent date.";
                        return false;
                    }

                    lateDays = (actualReturnDate.Date - expectedReturnDate.Date).Days;

                    if (lateDays < 0)
                    {
                        lateDays = 0;
                    }

                    fineAmount = lateDays * FinePerDay;

                    string updateBookingQuery = @"
                        UPDATE Bookings
                        SET 
                            ActualReturnDate = @ActualReturnDate,
                            BookingStatus = 'Completed'
                        WHERE BookingId = @BookingId
                        AND CustomerId = @CustomerId";

                    using (SqlCommand command = new SqlCommand(updateBookingQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@ActualReturnDate", actualReturnDate.Date);
                        command.Parameters.AddWithValue("@BookingId", bookingId);
                        command.Parameters.AddWithValue("@CustomerId", customerId);

                        command.ExecuteNonQuery();
                    }

                    string updateCarQuery = @"
                        UPDATE Cars
                        SET Status = 'Available'
                        WHERE CarId = @CarId";

                    using (SqlCommand command = new SqlCommand(updateCarQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@CarId", carId);
                        command.ExecuteNonQuery();
                    }

                    if (fineAmount > 0)
                    {
                        string insertFineQuery = @"
                            INSERT INTO Fines
                            (BookingId, LateDays, FineAmount, FineStatus)
                            VALUES
                            (@BookingId, @LateDays, @FineAmount, 'Unpaid')";

                        using (SqlCommand command = new SqlCommand(insertFineQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@BookingId", bookingId);
                            command.Parameters.AddWithValue("@LateDays", lateDays);
                            command.Parameters.AddWithValue("@FineAmount", fineAmount);

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();

                    if (fineAmount > 0)
                    {
                        message = "Car returned successfully. Late fine: " + fineAmount.ToString("0.00") + " BDT.";
                    }
                    else
                    {
                        message = "Car returned successfully. No fine.";
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    message = "Return failed: " + ex.Message;
                    return false;
                }
            }
        }

        public DataTable GetCustomerBookingHistory(int customerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        b.BookingId,
                        c.CarName,
                        c.Brand,
                        c.Model,
                        c.CarNumber,
                        u.FullName AS OwnerName,
                        b.RentDate,
                        b.ExpectedReturnDate,
                        b.ActualReturnDate,
                        b.TotalAmount,
                        b.DiscountAmount,
                        b.PayableAmount,
                        b.PaymentStatus,
                        ISNULL(f.FineAmount, 0) AS FineAmount,
                        ISNULL(f.FineStatus, '-') AS FineStatus,
                        b.BookingStatus
                    FROM Bookings b
                    INNER JOIN Cars c ON b.CarId = c.CarId
                    INNER JOIN Users u ON c.OwnerId = u.UserId
                    LEFT JOIN Fines f ON b.BookingId = f.BookingId
                    WHERE b.CustomerId = @CustomerId
                    ORDER BY b.BookingId DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public DataTable GetOwnerCarBookings(int ownerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        b.BookingId,
                        c.CarName,
                        c.Brand,
                        c.Model,
                        c.CarNumber,
                        customer.FullName AS CustomerName,
                        customer.Phone AS CustomerPhone,
                        b.RentDate,
                        b.ExpectedReturnDate,
                        b.ActualReturnDate,
                        b.TotalAmount,
                        b.DiscountAmount,
                        b.PayableAmount,
                        b.PaymentStatus,
                        ISNULL(f.FineAmount, 0) AS FineAmount,
                        ISNULL(f.FineStatus, '-') AS FineStatus,
                        b.BookingStatus
                    FROM Bookings b
                    INNER JOIN Cars c ON b.CarId = c.CarId
                    INNER JOIN Users customer ON b.CustomerId = customer.UserId
                    LEFT JOIN Fines f ON b.BookingId = f.BookingId
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
    }
}