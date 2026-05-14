using Car_Rent_Managment.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Car_Rent_Managment.Services
{
    public class UserService
    {
        public bool RegisterUser(
            string fullName,
            string email,
            string username,
            string password,
            string role,
            string phone,
            string address,
            out string message)
        {
            message = "";

            if (IsUsernameOrEmailExists(username, email))
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
                    (@FullName, @Email, @Username, @PasswordHash, @Role, @Phone, @Address, 'Active')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Address", address);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Registration successful. You can now login.";
                        return true;
                    }
                    else
                    {
                        message = "Registration failed.";
                        return false;
                    }
                }
            }
        }

        private bool IsUsernameOrEmailExists(string username, string email)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT COUNT(*)
                    FROM Users
                    WHERE Username = @Username OR Email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);

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