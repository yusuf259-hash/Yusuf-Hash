using Car_Rent_Managment.Data;
using Car_Rent_Managment.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Car_Rent_Managment.Services
{
    public class AuthService
    {
        public AuthenticatedUser Login(string usernameOrEmail, string password)
        {
            string passwordHash = GenerateSha256Hash(password);

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT TOP 1 UserId, FullName, Username, Role
                    FROM Users
                    WHERE (Username = @UsernameOrEmail OR Email = @UsernameOrEmail)
                    AND PasswordHash = @PasswordHash
                    AND Status = 'Active'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AuthenticatedUser
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FullName = reader["FullName"].ToString(),
                                Username = reader["Username"].ToString(),
                                Role = reader["Role"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
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