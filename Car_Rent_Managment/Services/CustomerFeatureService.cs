using Car_Rent_Managment.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Car_Rent_Managment.Services
{
    public class CustomerFeatureService
    {
        public DataTable GetCompletedBookingsWithoutReview(int customerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        b.BookingId,
                        b.CarId,
                        c.OwnerId,
                        c.CarName,
                        c.Brand,
                        c.Model,
                        c.CarNumber,
                        b.RentDate,
                        b.ExpectedReturnDate,
                        b.ActualReturnDate,
                        b.TotalAmount
                    FROM Bookings b
                    INNER JOIN Cars c ON b.CarId = c.CarId
                    LEFT JOIN Reviews r ON b.BookingId = r.BookingId
                    WHERE b.CustomerId = @CustomerId
                    AND b.BookingStatus = 'Completed'
                    AND r.ReviewId IS NULL
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

        public bool AddReview(
            int bookingId,
            int customerId,
            int carId,
            int ownerId,
            int rating,
            string comment,
            out string message)
        {
            message = "";

            if (rating < 1 || rating > 5)
            {
                message = "Rating must be between 1 and 5.";
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string checkQuery = @"
                    SELECT COUNT(*)
                    FROM Reviews
                    WHERE BookingId = @BookingId";

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@BookingId", bookingId);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        message = "Review already submitted for this booking.";
                        return false;
                    }
                }

                string insertQuery = @"
                    INSERT INTO Reviews
                    (BookingId, CustomerId, CarId, OwnerId, Rating, Comment)
                    VALUES
                    (@BookingId, @CustomerId, @CarId, @OwnerId, @Rating, @Comment)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@BookingId", bookingId);
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.Parameters.AddWithValue("@CarId", carId);
                    command.Parameters.AddWithValue("@OwnerId", ownerId);
                    command.Parameters.AddWithValue("@Rating", rating);
                    command.Parameters.AddWithValue("@Comment", comment);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Review submitted successfully.";
                        return true;
                    }

                    message = "Failed to submit review.";
                    return false;
                }
            }
        }

        public DataTable GetActiveOffersForCustomer()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        o.OfferId,
                        o.OfferTitle,
                        o.Description,
                        o.DiscountPercent,
                        o.StartDate,
                        o.EndDate,
                        o.OfferScope,
                        ISNULL(c.CarName, 'All Cars') AS CarName,
                        ISNULL(c.Brand, '-') AS Brand,
                        ISNULL(c.Model, '-') AS Model
                    FROM Offers o
                    LEFT JOIN Cars c ON o.CarId = c.CarId
                    WHERE o.Status = 'Active'
                    AND CAST(GETDATE() AS DATE) BETWEEN o.StartDate AND o.EndDate
                    ORDER BY o.OfferId DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public DataTable GetUnpaidBookings(int customerId)
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
                        b.RentDate,
                        b.ExpectedReturnDate,
                        b.TotalAmount,
                        b.DiscountAmount,
                        b.PayableAmount,
                        b.PaymentStatus
                    FROM Bookings b
                    INNER JOIN Cars c ON b.CarId = c.CarId
                    WHERE b.CustomerId = @CustomerId
                    AND b.PaymentStatus = 'Unpaid'
                    AND b.BookingStatus <> 'Cancelled'
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

        public bool MakePayment(
            int bookingId,
            int customerId,
            decimal amount,
            string paymentMethod,
            string transactionNumber,
            out string message)
        {
            message = "";

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string checkQuery = @"
                        SELECT COUNT(*)
                        FROM Bookings
                        WHERE BookingId = @BookingId
                        AND CustomerId = @CustomerId
                        AND PaymentStatus = 'Unpaid'
                        AND BookingStatus <> 'Cancelled'";

                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@BookingId", bookingId);
                        checkCommand.Parameters.AddWithValue("@CustomerId", customerId);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            transaction.Rollback();
                            message = "Valid unpaid booking not found.";
                            return false;
                        }
                    }

                    string insertPaymentQuery = @"
                        INSERT INTO Payments
                        (BookingId, CustomerId, Amount, PaymentMethod, TransactionNumber, PaymentStatus)
                        VALUES
                        (@BookingId, @CustomerId, @Amount, @PaymentMethod, @TransactionNumber, 'Paid')";

                    using (SqlCommand paymentCommand = new SqlCommand(insertPaymentQuery, connection, transaction))
                    {
                        paymentCommand.Parameters.AddWithValue("@BookingId", bookingId);
                        paymentCommand.Parameters.AddWithValue("@CustomerId", customerId);
                        paymentCommand.Parameters.AddWithValue("@Amount", amount);
                        paymentCommand.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        paymentCommand.Parameters.AddWithValue("@TransactionNumber", transactionNumber);

                        paymentCommand.ExecuteNonQuery();
                    }

                    string updateBookingQuery = @"
                        UPDATE Bookings
                        SET PaymentStatus = 'Paid'
                        WHERE BookingId = @BookingId
                        AND CustomerId = @CustomerId";

                    using (SqlCommand updateCommand = new SqlCommand(updateBookingQuery, connection, transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@BookingId", bookingId);
                        updateCommand.Parameters.AddWithValue("@CustomerId", customerId);

                        updateCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    message = "Payment completed successfully.";
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    message = "Payment failed: " + ex.Message;
                    return false;
                }
            }
        }
public DataTable GetCustomerBookingDashboard(int customerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT
                b.BookingId,
                b.CustomerId,
                b.CarId,
                c.OwnerId,
                c.CarName,
                c.Brand,
                c.Model,
                c.CarNumber,
                c.Location,
                owner.FullName AS OwnerName,
                b.RentDate,
                b.ExpectedReturnDate,
                b.ActualReturnDate,
                b.TotalAmount,
                b.DiscountAmount,
                b.PayableAmount,
                b.PaymentStatus,
                b.BookingStatus,
                ISNULL(f.FineId, 0) AS FineId,
                ISNULL(f.LateDays, 0) AS LateDays,
                ISNULL(f.FineAmount, 0) AS FineAmount,
                ISNULL(f.FineStatus, '-') AS FineStatus,
                ISNULL(r.ReviewId, 0) AS ReviewId,
                ISNULL(r.Rating, 0) AS Rating,
                ISNULL(r.Comment, '') AS ReviewComment
            FROM Bookings b
            INNER JOIN Cars c ON b.CarId = c.CarId
            INNER JOIN Users owner ON c.OwnerId = owner.UserId
            LEFT JOIN Fines f ON b.BookingId = f.BookingId
            LEFT JOIN Reviews r ON b.BookingId = r.BookingId
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

        public bool CancelUnpaidBooking(int customerId, int bookingId, out string message)
        {
            message = "";

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    int carId = 0;

                    string checkQuery = @"
                SELECT CarId
                FROM Bookings
                WHERE BookingId = @BookingId
                AND CustomerId = @CustomerId
                AND BookingStatus = 'Active'
                AND PaymentStatus = 'Unpaid'";

                    using (SqlCommand command = new SqlCommand(checkQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@BookingId", bookingId);
                        command.Parameters.AddWithValue("@CustomerId", customerId);

                        object result = command.ExecuteScalar();

                        if (result == null)
                        {
                            transaction.Rollback();
                            message = "Only active unpaid bookings can be cancelled.";
                            return false;
                        }

                        carId = Convert.ToInt32(result);
                    }

                    string updateBookingQuery = @"
                UPDATE Bookings
                SET BookingStatus = 'Cancelled'
                WHERE BookingId = @BookingId
                AND CustomerId = @CustomerId";

                    using (SqlCommand command = new SqlCommand(updateBookingQuery, connection, transaction))
                    {
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

                    transaction.Commit();

                    message = "Booking cancelled successfully.";
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    message = "Cancel failed: " + ex.Message;
                    return false;
                }
            }
        }

        public bool PayFine(
            int bookingId,
            int customerId,
            decimal amount,
            string paymentMethod,
            string transactionNumber,
            out string message)
        {
            message = "";

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string checkQuery = @"
                SELECT COUNT(*)
                FROM Bookings b
                INNER JOIN Fines f ON b.BookingId = f.BookingId
                WHERE b.BookingId = @BookingId
                AND b.CustomerId = @CustomerId
                AND f.FineStatus = 'Unpaid'";

                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@BookingId", bookingId);
                        checkCommand.Parameters.AddWithValue("@CustomerId", customerId);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            transaction.Rollback();
                            message = "Unpaid fine not found for this booking.";
                            return false;
                        }
                    }

                    string insertPaymentQuery = @"
                INSERT INTO Payments
                (BookingId, CustomerId, Amount, PaymentMethod, TransactionNumber, PaymentStatus)
                VALUES
                (@BookingId, @CustomerId, @Amount, @PaymentMethod, @TransactionNumber, 'Paid')";

                    using (SqlCommand paymentCommand = new SqlCommand(insertPaymentQuery, connection, transaction))
                    {
                        paymentCommand.Parameters.AddWithValue("@BookingId", bookingId);
                        paymentCommand.Parameters.AddWithValue("@CustomerId", customerId);
                        paymentCommand.Parameters.AddWithValue("@Amount", amount);
                        paymentCommand.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        paymentCommand.Parameters.AddWithValue("@TransactionNumber", transactionNumber);

                        paymentCommand.ExecuteNonQuery();
                    }

                    string updateFineQuery = @"
                UPDATE Fines
                SET FineStatus = 'Paid'
                WHERE BookingId = @BookingId";

                    using (SqlCommand command = new SqlCommand(updateFineQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@BookingId", bookingId);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    message = "Fine paid successfully.";
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    message = "Fine payment failed: " + ex.Message;
                    return false;
                }
            }
        } } }