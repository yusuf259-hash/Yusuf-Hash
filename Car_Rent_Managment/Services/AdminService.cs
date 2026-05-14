using Car_Rent_Managment.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Car_Rent_Managment.Services
{
    public class AdminService
    {
        public DataTable GetUsersForManager(string managerRole)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query;

                if (managerRole == "SuperAdmin")
                {
                    query = @"
                        SELECT 
                            UserId,
                            FullName,
                            Email,
                            Username,
                            Role,
                            Phone,
                            Address,
                            Status,
                            CreatedAt
                        FROM Users
                        ORDER BY UserId DESC";
                }
                else
                {
                    query = @"
                        SELECT 
                            UserId,
                            FullName,
                            Email,
                            Username,
                            Role,
                            Phone,
                            Address,
                            Status,
                            CreatedAt
                        FROM Users
                        WHERE Role IN ('Customer', 'Owner')
                        ORDER BY UserId DESC";
                }

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

        public DataTable GetPlatformSummary()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        (SELECT COUNT(*) FROM Users) AS TotalUsers,
                        (SELECT COUNT(*) FROM Users WHERE Role = 'Customer') AS Customers,
                        (SELECT COUNT(*) FROM Users WHERE Role = 'Owner') AS Owners,
                        (SELECT COUNT(*) FROM Users WHERE Role = 'Admin') AS Admins,
                        (SELECT COUNT(*) FROM Users WHERE Status = 'Suspended') AS SuspendedUsers,
                        (SELECT COUNT(*) FROM Cars) AS TotalCars,
                        (SELECT COUNT(*) FROM Cars WHERE Status = 'Available') AS AvailableCars,
                        (SELECT COUNT(*) FROM Cars WHERE Status = 'Rented') AS RentedCars,
                        (SELECT COUNT(*) FROM Cars WHERE Status IN ('Maintenance', 'Unavailable')) AS InactiveCars,
                        (SELECT COUNT(*) FROM Bookings) AS TotalBookings,
                        (SELECT COUNT(*) FROM Bookings WHERE BookingStatus = 'Active') AS ActiveBookings,
                        (SELECT COUNT(*) FROM Bookings WHERE BookingStatus = 'Completed') AS CompletedBookings,
                        (SELECT COUNT(*) FROM Bookings WHERE BookingStatus = 'Cancelled') AS CancelledBookings,
                        (SELECT COUNT(*) FROM Bookings WHERE PaymentStatus = 'Paid') AS PaidBookings,
                        (SELECT COUNT(*) FROM Bookings WHERE PaymentStatus = 'Unpaid') AS UnpaidBookings,
                        (SELECT ISNULL(SUM(PayableAmount), 0) FROM Bookings WHERE PaymentStatus = 'Paid' AND BookingStatus <> 'Cancelled') AS Revenue,
                        (SELECT COUNT(*) FROM Reviews) AS Reviews,
                        (SELECT COUNT(*) FROM Reviews WHERE Rating <= 2) AS LowRatings,
                        (SELECT COUNT(*) FROM Reviews WHERE Rating = 5) AS FiveStarReviews,
                        (SELECT ISNULL(AVG(CAST(Rating AS DECIMAL(5,2))), 0) FROM Reviews) AS AverageRating,
                        (SELECT COUNT(*) FROM Offers) AS TotalOffers,
                        (SELECT COUNT(*) FROM Offers WHERE Status = 'Active') AS ActiveOffers,
                        (SELECT COUNT(*) FROM Offers WHERE Status = 'Inactive') AS InactiveOffers,
                        (SELECT COUNT(*) FROM Offers WHERE OfferScope = 'Platform') AS PlatformOffers,
                        (SELECT COUNT(*) FROM Offers WHERE OfferScope IN ('Owner', 'Car')) AS OwnerCarOffers";

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

        public DataTable GetUserSummaryForManager(string managerRole)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        (SELECT COUNT(*) FROM Users WHERE (@IsSuperAdmin = 1 OR Role IN ('Customer', 'Owner'))) AS TotalUsers,
                        (SELECT COUNT(*) FROM Users WHERE Role = 'Customer') AS Customers,
                        (SELECT COUNT(*) FROM Users WHERE Role = 'Owner') AS Owners,
                        (SELECT COUNT(*) FROM Users WHERE Role = 'Admin' AND @IsSuperAdmin = 1) AS Admins,
                        (SELECT COUNT(*) FROM Users WHERE Status = 'Suspended' AND (@IsSuperAdmin = 1 OR Role IN ('Customer', 'Owner'))) AS SuspendedUsers";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IsSuperAdmin", managerRole == "SuperAdmin" ? 1 : 0);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public DataTable GetCarSummary()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        COUNT(*) AS TotalCars,
                        SUM(CASE WHEN Status = 'Available' THEN 1 ELSE 0 END) AS AvailableCars,
                        SUM(CASE WHEN Status = 'Rented' THEN 1 ELSE 0 END) AS RentedCars,
                        SUM(CASE WHEN Status IN ('Maintenance', 'Unavailable') THEN 1 ELSE 0 END) AS InactiveCars
                    FROM Cars";

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

        public DataTable GetBookingSummary()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        COUNT(*) AS TotalBookings,
                        SUM(CASE WHEN BookingStatus = 'Active' THEN 1 ELSE 0 END) AS ActiveBookings,
                        SUM(CASE WHEN BookingStatus = 'Completed' THEN 1 ELSE 0 END) AS CompletedBookings,
                        SUM(CASE WHEN BookingStatus = 'Cancelled' THEN 1 ELSE 0 END) AS CancelledBookings,
                        SUM(CASE WHEN PaymentStatus = 'Paid' THEN 1 ELSE 0 END) AS PaidBookings,
                        SUM(CASE WHEN PaymentStatus = 'Unpaid' THEN 1 ELSE 0 END) AS UnpaidBookings
                    FROM Bookings";

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

        public bool AddUser(
            string managerRole,
            string fullName,
            string email,
            string username,
            string password,
            string role,
            string phone,
            string address,
            string status,
            out string message)
        {
            message = "";

            if (!IsRoleAllowedForManager(managerRole, role))
            {
                message = "You are not allowed to create this role.";
                return false;
            }

            if (IsUsernameOrEmailExists(username, email, 0))
            {
                message = "Username or email already exists.";
                return false;
            }

            string passwordHash = GenerateSha256Hash(password);

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    INSERT INTO Users
                    (FullName, Email, Username, PasswordHash, Role, Phone, Address, Status)
                    VALUES
                    (@FullName, @Email, @Username, @PasswordHash, @Role, @Phone, @Address, @Status)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Status", status);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "User added successfully.";
                        return true;
                    }

                    message = "Failed to add user.";
                    return false;
                }
            }
        }

        public bool UpdateUser(
            string managerRole,
            int targetUserId,
            int currentUserId,
            string fullName,
            string email,
            string username,
            string password,
            string role,
            string phone,
            string address,
            string status,
            out string message)
        {
            message = "";

            if (targetUserId == currentUserId && status == "Suspended")
            {
                message = "You cannot suspend your own account.";
                return false;
            }

            string existingRole = GetUserRole(targetUserId);

            if (string.IsNullOrWhiteSpace(existingRole))
            {
                message = "User not found.";
                return false;
            }

            if (managerRole == "Admin" && (existingRole == "Admin" || existingRole == "SuperAdmin"))
            {
                message = "Admin cannot manage Admin or SuperAdmin accounts.";
                return false;
            }

            if (!IsRoleAllowedForManager(managerRole, role))
            {
                message = "You are not allowed to assign this role.";
                return false;
            }

            if (IsUsernameOrEmailExists(username, email, targetUserId))
            {
                message = "Username or email already exists.";
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query;

                if (string.IsNullOrWhiteSpace(password))
                {
                    query = @"
                        UPDATE Users
                        SET 
                            FullName = @FullName,
                            Email = @Email,
                            Username = @Username,
                            Role = @Role,
                            Phone = @Phone,
                            Address = @Address,
                            Status = @Status
                        WHERE UserId = @UserId";
                }
                else
                {
                    query = @"
                        UPDATE Users
                        SET 
                            FullName = @FullName,
                            Email = @Email,
                            Username = @Username,
                            PasswordHash = @PasswordHash,
                            Role = @Role,
                            Phone = @Phone,
                            Address = @Address,
                            Status = @Status
                        WHERE UserId = @UserId";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", targetUserId);
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Status", status);

                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        command.Parameters.AddWithValue("@PasswordHash", GenerateSha256Hash(password));
                    }

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "User updated successfully.";
                        return true;
                    }

                    message = "Failed to update user.";
                    return false;
                }
            }
        }

        public bool SuspendUser(string managerRole, int targetUserId, int currentUserId, out string message)
        {
            return ChangeUserStatus(managerRole, targetUserId, currentUserId, "Suspended", out message);
        }

        public bool ActivateUser(string managerRole, int targetUserId, int currentUserId, out string message)
        {
            return ChangeUserStatus(managerRole, targetUserId, currentUserId, "Active", out message);
        }

        private bool ChangeUserStatus(string managerRole, int targetUserId, int currentUserId, string status, out string message)
        {
            message = "";

            if (targetUserId == currentUserId && status == "Suspended")
            {
                message = "You cannot suspend your own account.";
                return false;
            }

            string targetRole = GetUserRole(targetUserId);

            if (string.IsNullOrWhiteSpace(targetRole))
            {
                message = "User not found.";
                return false;
            }

            if (targetRole == "SuperAdmin")
            {
                message = "SuperAdmin account cannot be suspended here.";
                return false;
            }

            if (managerRole == "Admin" && (targetRole == "Admin" || targetRole == "SuperAdmin"))
            {
                message = "Admin cannot manage Admin or SuperAdmin accounts.";
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "UPDATE Users SET Status = @Status WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@UserId", targetUserId);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "User status updated successfully.";
                        return true;
                    }

                    message = "Failed to update user status.";
                    return false;
                }
            }
        }

        public bool DeleteUserSafe(string managerRole, int targetUserId, int currentUserId, out string message)
        {
            message = "";

            if (targetUserId == currentUserId)
            {
                message = "You cannot delete your own account.";
                return false;
            }

            string targetRole = GetUserRole(targetUserId);

            if (string.IsNullOrWhiteSpace(targetRole))
            {
                message = "User not found.";
                return false;
            }

            if (targetRole == "SuperAdmin")
            {
                message = "SuperAdmin account cannot be deleted.";
                return false;
            }

            if (managerRole == "Admin" && (targetRole == "Admin" || targetRole == "SuperAdmin"))
            {
                message = "Admin cannot delete Admin or SuperAdmin accounts.";
                return false;
            }

            if (UserHasRelatedRecords(targetUserId))
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string suspendQuery = "UPDATE Users SET Status = 'Suspended' WHERE UserId = @UserId";

                    using (SqlCommand command = new SqlCommand(suspendQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", targetUserId);
                        command.ExecuteNonQuery();
                    }
                }

                message = "User has cars or bookings, so the account was suspended instead of deleted.";
                return true;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM Users WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", targetUserId);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "User deleted successfully.";
                        return true;
                    }

                    message = "Failed to delete user.";
                    return false;
                }
            }
        }

        public DataTable GetAllCars()
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
                        c.Description,
                        u.FullName AS OwnerName,
                        u.Phone AS OwnerPhone
                    FROM Cars c
                    INNER JOIN Users u ON c.OwnerId = u.UserId
                    ORDER BY c.CarId DESC";

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

        public bool UpdateCarStatus(int carId, string status, out string message)
        {
            message = "";

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "UPDATE Cars SET Status = @Status WHERE CarId = @CarId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@CarId", carId);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Car status updated successfully.";
                        return true;
                    }

                    message = "Failed to update car status.";
                    return false;
                }
            }
        }

        public bool DeleteCarSafe(int carId, out string message)
        {
            message = "";

            if (CarHasBookings(carId))
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "UPDATE Cars SET Status = 'Unavailable' WHERE CarId = @CarId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CarId", carId);
                        command.ExecuteNonQuery();
                    }
                }

                message = "Car has booking history, so it was marked as Unavailable instead of deleted.";
                return true;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM Cars WHERE CarId = @CarId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CarId", carId);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Car deleted successfully.";
                        return true;
                    }

                    message = "Failed to delete car.";
                    return false;
                }
            }
        }

        public DataTable GetAllBookings()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        b.BookingId,
                        customer.FullName AS CustomerName,
                        customer.Phone AS CustomerPhone,
                        c.CarName,
                        c.Brand,
                        c.Model,
                        c.CarNumber,
                        owner.FullName AS OwnerName,
                        b.RentDate,
                        b.ExpectedReturnDate,
                        b.ActualReturnDate,
                        b.TotalAmount,
                        ISNULL(f.FineAmount, 0) AS FineAmount,
                        ISNULL(f.FineStatus, '-') AS FineStatus,
                        b.BookingStatus
                    FROM Bookings b
                    INNER JOIN Users customer ON b.CustomerId = customer.UserId
                    INNER JOIN Cars c ON b.CarId = c.CarId
                    INNER JOIN Users owner ON c.OwnerId = owner.UserId
                    LEFT JOIN Fines f ON b.BookingId = f.BookingId
                    ORDER BY b.BookingId DESC";

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

        public bool CancelBooking(int bookingId, out string message)
        {
            message = "";

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    int carId = 0;
                    string bookingStatus = "";

                    string getQuery = @"
                        SELECT CarId, BookingStatus
                        FROM Bookings
                        WHERE BookingId = @BookingId";

                    using (SqlCommand command = new SqlCommand(getQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@BookingId", bookingId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                reader.Close();
                                transaction.Rollback();
                                message = "Booking not found.";
                                return false;
                            }

                            carId = Convert.ToInt32(reader["CarId"]);
                            bookingStatus = reader["BookingStatus"].ToString() ?? "";
                        }
                    }

                    if (bookingStatus != "Active")
                    {
                        transaction.Rollback();
                        message = "Only active bookings can be cancelled.";
                        return false;
                    }

                    string cancelQuery = @"
                        UPDATE Bookings
                        SET BookingStatus = 'Cancelled'
                        WHERE BookingId = @BookingId";

                    using (SqlCommand command = new SqlCommand(cancelQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@BookingId", bookingId);
                        command.ExecuteNonQuery();
                    }

                    string carQuery = @"
                        UPDATE Cars
                        SET Status = 'Available'
                        WHERE CarId = @CarId";

                    using (SqlCommand command = new SqlCommand(carQuery, connection, transaction))
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

        public bool MarkFinePaid(int bookingId, out string message)
        {
            message = "";

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM Fines WHERE BookingId = @BookingId";

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@BookingId", bookingId);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count == 0)
                    {
                        message = "No fine exists for this booking.";
                        return false;
                    }
                }

                string query = @"
                    UPDATE Fines
                    SET FineStatus = 'Paid'
                    WHERE BookingId = @BookingId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookingId", bookingId);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Fine marked as paid.";
                        return true;
                    }

                    message = "Failed to update fine.";
                    return false;
                }
            }
        }

        public DataTable GetReports()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Report Item");
            dataTable.Columns.Add("Value");

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                AddReportRow(dataTable, "Total Users", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Users"));
                AddReportRow(dataTable, "Total Customers", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Users WHERE Role = 'Customer'"));
                AddReportRow(dataTable, "Total Owners", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Users WHERE Role = 'Owner'"));
                AddReportRow(dataTable, "Total Admins", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Users WHERE Role = 'Admin'"));
                AddReportRow(dataTable, "Total Cars", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Cars"));
                AddReportRow(dataTable, "Available Cars", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Cars WHERE Status = 'Available'"));
                AddReportRow(dataTable, "Rented Cars", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Cars WHERE Status = 'Rented'"));
                AddReportRow(dataTable, "Total Bookings", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Bookings"));
                AddReportRow(dataTable, "Active Bookings", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Bookings WHERE BookingStatus = 'Active'"));
                AddReportRow(dataTable, "Completed Bookings", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Bookings WHERE BookingStatus = 'Completed'"));
                AddReportRow(dataTable, "Cancelled Bookings", ExecuteScalarText(connection, "SELECT COUNT(*) FROM Bookings WHERE BookingStatus = 'Cancelled'"));
                AddReportRow(dataTable, "Total Rental Revenue", ExecuteScalarText(connection, "SELECT ISNULL(SUM(TotalAmount), 0) FROM Bookings WHERE BookingStatus <> 'Cancelled'") + " BDT");
                AddReportRow(dataTable, "Total Fine Amount", ExecuteScalarText(connection, "SELECT ISNULL(SUM(FineAmount), 0) FROM Fines") + " BDT");
                AddReportRow(dataTable, "Unpaid Fine Amount", ExecuteScalarText(connection, "SELECT ISNULL(SUM(FineAmount), 0) FROM Fines WHERE FineStatus = 'Unpaid'") + " BDT");
            }

            return dataTable;
        }

        private void AddReportRow(DataTable table, string item, string value)
        {
            DataRow row = table.NewRow();
            row["Report Item"] = item;
            row["Value"] = value;
            table.Rows.Add(row);
        }

        private string ExecuteScalarText(SqlConnection connection, string query)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                return Convert.ToString(result) ?? "0";
            }
        }

        private bool IsRoleAllowedForManager(string managerRole, string targetRole)
        {
            if (managerRole == "SuperAdmin")
            {
                return targetRole == "Customer" || targetRole == "Owner" || targetRole == "Admin";
            }

            if (managerRole == "Admin")
            {
                return targetRole == "Customer" || targetRole == "Owner";
            }

            return false;
        }

        private string GetUserRole(int userId)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT Role FROM Users WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        return "";
                    }

                    return result.ToString() ?? "";
                }
            }
        }

        private bool IsUsernameOrEmailExists(string username, string email, int currentUserId)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT COUNT(*)
                    FROM Users
                    WHERE (Username = @Username OR Email = @Email)
                    AND UserId <> @CurrentUserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@CurrentUserId", currentUserId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private bool UserHasRelatedRecords(int userId)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                    (
                        SELECT COUNT(*) FROM Cars WHERE OwnerId = @UserId
                    )
                    +
                    (
                        SELECT COUNT(*) FROM Bookings WHERE CustomerId = @UserId
                    )";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private bool CarHasBookings(int carId)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Bookings WHERE CarId = @CarId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CarId", carId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private string GenerateSha256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("X2"));
                }

                return builder.ToString();
            }
        }
    }
}
