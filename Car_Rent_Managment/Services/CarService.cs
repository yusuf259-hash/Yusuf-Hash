using Car_Rent_Managment.Data;
using Car_Rent_Managment.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Car_Rent_Managment.Services
{
    public class CarService
    {
        public DataTable GetCarsByOwner(int ownerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT 
                        CarId,
                        CarName,
                        Brand,
                        Model,
                        CarNumber,
                        Seats,
                        PricePerDay,
                        Location,
                        Status,
                        Description
                    FROM Cars
                    WHERE OwnerId = @OwnerId
                    ORDER BY CarId DESC";

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

        public bool AddCar(Car car, out string message)
        {
            message = "";

            if (IsCarNumberExists(car.CarNumber, 0))
            {
                message = "This car number already exists.";
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    INSERT INTO Cars
                    (OwnerId, CarName, Brand, Model, CarNumber, Seats, PricePerDay, Location, Status, Description)
                    VALUES
                    (@OwnerId, @CarName, @Brand, @Model, @CarNumber, @Seats, @PricePerDay, @Location, @Status, @Description)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OwnerId", car.OwnerId);
                    command.Parameters.AddWithValue("@CarName", car.CarName);
                    command.Parameters.AddWithValue("@Brand", car.Brand);
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@CarNumber", car.CarNumber);
                    command.Parameters.AddWithValue("@Seats", car.Seats);
                    command.Parameters.AddWithValue("@PricePerDay", car.PricePerDay);
                    command.Parameters.AddWithValue("@Location", car.Location);
                    command.Parameters.AddWithValue("@Status", car.Status);
                    command.Parameters.AddWithValue("@Description", car.Description ?? "");

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Car added successfully.";
                        return true;
                    }

                    message = "Failed to add car.";
                    return false;
                }
            }
        }

        public bool UpdateCar(Car car, out string message)
        {
            message = "";

            if (IsCarNumberExists(car.CarNumber, car.CarId))
            {
                message = "This car number is already used by another car.";
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    UPDATE Cars
                    SET 
                        CarName = @CarName,
                        Brand = @Brand,
                        Model = @Model,
                        CarNumber = @CarNumber,
                        Seats = @Seats,
                        PricePerDay = @PricePerDay,
                        Location = @Location,
                        Status = @Status,
                        Description = @Description
                    WHERE CarId = @CarId AND OwnerId = @OwnerId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CarId", car.CarId);
                    command.Parameters.AddWithValue("@OwnerId", car.OwnerId);
                    command.Parameters.AddWithValue("@CarName", car.CarName);
                    command.Parameters.AddWithValue("@Brand", car.Brand);
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@CarNumber", car.CarNumber);
                    command.Parameters.AddWithValue("@Seats", car.Seats);
                    command.Parameters.AddWithValue("@PricePerDay", car.PricePerDay);
                    command.Parameters.AddWithValue("@Location", car.Location);
                    command.Parameters.AddWithValue("@Status", car.Status);
                    command.Parameters.AddWithValue("@Description", car.Description ?? "");

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Car updated successfully.";
                        return true;
                    }

                    message = "Failed to update car.";
                    return false;
                }
            }
        }

        public bool DeleteCar(int carId, int ownerId, out string message)
        {
            message = "";

            if (HasBookings(carId))
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string updateQuery = @"
                        UPDATE Cars
                        SET Status = 'Unavailable'
                        WHERE CarId = @CarId AND OwnerId = @OwnerId";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CarId", carId);
                        command.Parameters.AddWithValue("@OwnerId", ownerId);

                        command.ExecuteNonQuery();
                    }
                }

                message = "This car has booking history, so it cannot be deleted. It has been marked as Unavailable.";
                return true;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM Cars WHERE CarId = @CarId AND OwnerId = @OwnerId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CarId", carId);
                    command.Parameters.AddWithValue("@OwnerId", ownerId);

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

        private bool IsCarNumberExists(string carNumber, int currentCarId)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT COUNT(*)
                    FROM Cars
                    WHERE CarNumber = @CarNumber
                    AND CarId <> @CurrentCarId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CarNumber", carNumber);
                    command.Parameters.AddWithValue("@CurrentCarId", currentCarId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private bool HasBookings(int carId)
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
    }
}