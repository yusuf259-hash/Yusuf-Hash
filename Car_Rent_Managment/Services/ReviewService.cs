using Car_Rent_Managment.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Car_Rent_Managment.Services
{
    public class ReviewService
    {
        public DataTable GetReviews(int rating, string customerName, string ownerName, string carName)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        r.ReviewId,
                        r.BookingId,
                        customer.FullName AS CustomerName,
                        owner.FullName AS OwnerName,
                        c.CarName,
                        r.Rating,
                        r.Comment,
                        r.CreatedAt,
                        r.OwnerId
                    FROM Reviews r
                    INNER JOIN Users customer ON r.CustomerId = customer.UserId
                    INNER JOIN Users owner ON r.OwnerId = owner.UserId
                    INNER JOIN Cars c ON r.CarId = c.CarId
                    WHERE (@Rating = 0 OR r.Rating = @Rating)
                    AND (@CustomerName = '' OR customer.FullName LIKE '%' + @CustomerName + '%')
                    AND (@OwnerName = '' OR owner.FullName LIKE '%' + @OwnerName + '%')
                    AND (@CarName = '' OR c.CarName LIKE '%' + @CarName + '%')
                    ORDER BY r.CreatedAt DESC, r.ReviewId DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Rating", rating);
                    command.Parameters.AddWithValue("@CustomerName", customerName.Trim());
                    command.Parameters.AddWithValue("@OwnerName", ownerName.Trim());
                    command.Parameters.AddWithValue("@CarName", carName.Trim());

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public DataTable GetReviewSummary()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        COUNT(*) AS TotalReviews,
                        ISNULL(AVG(CAST(Rating AS DECIMAL(5,2))), 0) AS AverageRating,
                        SUM(CASE WHEN Rating <= 2 THEN 1 ELSE 0 END) AS LowRatings,
                        SUM(CASE WHEN Rating = 5 THEN 1 ELSE 0 END) AS FiveStarReviews
                    FROM Reviews";

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

        public bool DeleteReview(int reviewId, out string message)
        {
            message = "";

            if (reviewId <= 0)
            {
                message = "Please select a review to delete.";
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    DELETE FROM Reviews
                    WHERE ReviewId = @ReviewId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReviewId", reviewId);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Review deleted successfully.";
                        return true;
                    }
                }
            }

            message = "Review not found.";
            return false;
        }

        public bool SuspendOwnerFromReview(int reviewId, string currentUserRole, out string message)
        {
            message = "";

            if (!string.Equals(currentUserRole, "SuperAdmin", StringComparison.OrdinalIgnoreCase))
            {
                message = "Only SuperAdmin can suspend owners from this page.";
                return false;
            }

            if (reviewId <= 0)
            {
                message = "Please select a review first.";
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    UPDATE ownerUser
                    SET Status = 'Suspended'
                    FROM Users ownerUser
                    INNER JOIN Reviews r ON r.OwnerId = ownerUser.UserId
                    WHERE r.ReviewId = @ReviewId
                    AND ownerUser.Role = 'Owner'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReviewId", reviewId);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Owner suspended successfully.";
                        return true;
                    }
                }
            }

            message = "Owner not found for the selected review.";
            return false;
        }
    }
}
