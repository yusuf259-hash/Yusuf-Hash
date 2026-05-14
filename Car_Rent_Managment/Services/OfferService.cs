using Car_Rent_Managment.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Car_Rent_Managment.Services
{
    public class OfferService
    {
        public DataTable GetOwnerOffers(int ownerId)
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
                        o.CarId,
                        ISNULL(c.CarName + ' (' + c.CarNumber + ')', 'All My Cars') AS AppliesTo,
                        o.Status,
                        o.CreatedAt
                    FROM Offers o
                    LEFT JOIN Cars c ON o.CarId = c.CarId
                    WHERE o.CreatedByUserId = @OwnerId
                    AND o.OfferScope IN ('Owner', 'Car')
                    ORDER BY o.OfferId DESC";

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

        public DataTable GetOwnerOfferSummary(int ownerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        COUNT(*) AS TotalOffers,
                        SUM(CASE WHEN Status = 'Active' THEN 1 ELSE 0 END) AS ActiveOffers,
                        SUM(CASE WHEN Status IN ('Inactive', 'Expired') THEN 1 ELSE 0 END) AS InactiveOffers
                    FROM Offers
                    WHERE CreatedByUserId = @OwnerId
                    AND OfferScope IN ('Owner', 'Car')";

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

        public DataTable GetOwnerCarsForOffers(int ownerId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        CarId,
                        CarName + ' - ' + CarNumber AS DisplayName
                    FROM Cars
                    WHERE OwnerId = @OwnerId
                    ORDER BY CarName";

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

        public bool AddOwnerOffer(
            int ownerId,
            string offerTitle,
            string description,
            decimal discountPercent,
            DateTime startDate,
            DateTime endDate,
            int? carId,
            string status,
            out string message)
        {
            message = "";

            if (!ValidateOfferInput(ownerId, offerTitle, discountPercent, startDate, endDate, status, out message))
            {
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                if (carId.HasValue && !IsOwnerCar(connection, ownerId, carId.Value))
                {
                    message = "Selected car does not belong to this owner.";
                    return false;
                }

                string offerScope = carId.HasValue ? "Car" : "Owner";

                string query = @"
                    INSERT INTO Offers
                    (
                        OfferTitle,
                        Description,
                        DiscountPercent,
                        StartDate,
                        EndDate,
                        OfferScope,
                        CarId,
                        CreatedByUserId,
                        Status
                    )
                    VALUES
                    (
                        @OfferTitle,
                        @Description,
                        @DiscountPercent,
                        @StartDate,
                        @EndDate,
                        @OfferScope,
                        @CarId,
                        @CreatedByUserId,
                        @Status
                    )";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddOfferParameters(command, ownerId, offerTitle, description, discountPercent, startDate, endDate, carId, offerScope, status);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Offer added successfully.";
                        return true;
                    }
                }
            }

            message = "Failed to add offer.";
            return false;
        }

        public bool UpdateOwnerOffer(
            int ownerId,
            int offerId,
            string offerTitle,
            string description,
            decimal discountPercent,
            DateTime startDate,
            DateTime endDate,
            int? carId,
            string status,
            out string message)
        {
            message = "";

            if (offerId <= 0)
            {
                message = "Please select an offer to update.";
                return false;
            }

            if (!ValidateOfferInput(ownerId, offerTitle, discountPercent, startDate, endDate, status, out message))
            {
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                if (!IsOwnerOffer(connection, ownerId, offerId))
                {
                    message = "Offer not found for this owner.";
                    return false;
                }

                if (carId.HasValue && !IsOwnerCar(connection, ownerId, carId.Value))
                {
                    message = "Selected car does not belong to this owner.";
                    return false;
                }

                string offerScope = carId.HasValue ? "Car" : "Owner";

                string query = @"
                    UPDATE Offers
                    SET
                        OfferTitle = @OfferTitle,
                        Description = @Description,
                        DiscountPercent = @DiscountPercent,
                        StartDate = @StartDate,
                        EndDate = @EndDate,
                        OfferScope = @OfferScope,
                        CarId = @CarId,
                        Status = @Status
                    WHERE OfferId = @OfferId
                    AND CreatedByUserId = @CreatedByUserId
                    AND OfferScope IN ('Owner', 'Car')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OfferId", offerId);
                    AddOfferParameters(command, ownerId, offerTitle, description, discountPercent, startDate, endDate, carId, offerScope, status);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Offer updated successfully.";
                        return true;
                    }
                }
            }

            message = "Failed to update offer.";
            return false;
        }

        public bool DeactivateOwnerOffer(int ownerId, int offerId, out string message)
        {
            message = "";

            if (ownerId <= 0)
            {
                message = "Owner login is required.";
                return false;
            }

            if (offerId <= 0)
            {
                message = "Please select an offer to deactivate.";
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    UPDATE Offers
                    SET Status = 'Inactive'
                    WHERE OfferId = @OfferId
                    AND CreatedByUserId = @OwnerId
                    AND OfferScope IN ('Owner', 'Car')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OfferId", offerId);
                    command.Parameters.AddWithValue("@OwnerId", ownerId);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Offer deactivated successfully.";
                        return true;
                    }
                }
            }

            message = "Offer not found for this owner.";
            return false;
        }

        public DataTable GetAllOffersForAdmin()
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
                        o.CarId,
                        CASE
                            WHEN o.OfferScope = 'Platform' THEN 'All Platform Cars'
                            WHEN o.CarId IS NULL THEN 'All Owner Cars'
                            ELSE ISNULL(c.CarName + ' (' + c.CarNumber + ')', 'Selected Car')
                        END AS AppliesTo,
                        o.CreatedByUserId,
                        creator.FullName AS CreatedBy,
                        creator.Role AS CreatorRole,
                        o.Status,
                        o.CreatedAt
                    FROM Offers o
                    INNER JOIN Users creator ON o.CreatedByUserId = creator.UserId
                    LEFT JOIN Cars c ON o.CarId = c.CarId
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

        public DataTable GetOfferSummaryForAdmin()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT
                        COUNT(*) AS TotalOffers,
                        SUM(CASE WHEN Status = 'Active' THEN 1 ELSE 0 END) AS ActiveOffers,
                        SUM(CASE WHEN Status IN ('Inactive', 'Expired') THEN 1 ELSE 0 END) AS InactiveOffers,
                        SUM(CASE WHEN OfferScope = 'Platform' THEN 1 ELSE 0 END) AS PlatformOffers,
                        SUM(CASE WHEN OfferScope IN ('Owner', 'Car') THEN 1 ELSE 0 END) AS OwnerCarOffers
                    FROM Offers";

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

        public bool AddPlatformOffer(
            int adminUserId,
            string offerTitle,
            string description,
            decimal discountPercent,
            DateTime startDate,
            DateTime endDate,
            string status,
            out string message)
        {
            message = "";

            if (!ValidateAdminOfferInput(adminUserId, offerTitle, discountPercent, startDate, endDate, status, out message))
            {
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    INSERT INTO Offers
                    (
                        OfferTitle,
                        Description,
                        DiscountPercent,
                        StartDate,
                        EndDate,
                        OfferScope,
                        CarId,
                        CreatedByUserId,
                        Status
                    )
                    VALUES
                    (
                        @OfferTitle,
                        @Description,
                        @DiscountPercent,
                        @StartDate,
                        @EndDate,
                        'Platform',
                        NULL,
                        @CreatedByUserId,
                        @Status
                    )";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OfferTitle", offerTitle.Trim());
                    command.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description.Trim());
                    command.Parameters.AddWithValue("@DiscountPercent", discountPercent);
                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);
                    command.Parameters.AddWithValue("@CreatedByUserId", adminUserId);
                    command.Parameters.AddWithValue("@Status", status);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Platform offer added successfully.";
                        return true;
                    }
                }
            }

            message = "Failed to add platform offer.";
            return false;
        }

        public bool UpdateAdminOffer(
            int adminUserId,
            int offerId,
            string offerTitle,
            string description,
            decimal discountPercent,
            DateTime startDate,
            DateTime endDate,
            string status,
            out string message)
        {
            message = "";

            if (offerId <= 0)
            {
                message = "Please select an offer to update.";
                return false;
            }

            if (!ValidateAdminOfferInput(adminUserId, offerTitle, discountPercent, startDate, endDate, status, out message))
            {
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    UPDATE Offers
                    SET
                        OfferTitle = @OfferTitle,
                        Description = @Description,
                        DiscountPercent = @DiscountPercent,
                        StartDate = @StartDate,
                        EndDate = @EndDate,
                        Status = @Status
                    WHERE OfferId = @OfferId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OfferId", offerId);
                    command.Parameters.AddWithValue("@OfferTitle", offerTitle.Trim());
                    command.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description.Trim());
                    command.Parameters.AddWithValue("@DiscountPercent", discountPercent);
                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);
                    command.Parameters.AddWithValue("@Status", status);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Offer updated successfully.";
                        return true;
                    }
                }
            }

            message = "Offer not found.";
            return false;
        }

        public bool SetAdminOfferStatus(int adminUserId, int offerId, string status, out string message)
        {
            message = "";

            if (adminUserId <= 0)
            {
                message = "Admin or SuperAdmin login is required.";
                return false;
            }

            if (offerId <= 0)
            {
                message = "Please select an offer.";
                return false;
            }

            if (status != "Active" && status != "Inactive")
            {
                message = "Only Active or Inactive status can be set from this action.";
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    UPDATE Offers
                    SET Status = @Status
                    WHERE OfferId = @OfferId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@OfferId", offerId);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Offer status updated successfully.";
                        return true;
                    }
                }
            }

            message = "Offer not found.";
            return false;
        }

        public bool DeleteAdminOffer(int adminUserId, int offerId, out string message)
        {
            message = "";

            if (adminUserId <= 0)
            {
                message = "Admin or SuperAdmin login is required.";
                return false;
            }

            if (offerId <= 0)
            {
                message = "Please select an offer to delete.";
                return false;
            }

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string statusQuery = @"
                    SELECT Status
                    FROM Offers
                    WHERE OfferId = @OfferId";

                using (SqlCommand statusCommand = new SqlCommand(statusQuery, connection))
                {
                    statusCommand.Parameters.AddWithValue("@OfferId", offerId);
                    object? result = statusCommand.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        message = "Offer not found.";
                        return false;
                    }

                    string offerStatus = result.ToString() ?? "";
                    if (offerStatus == "Active")
                    {
                        message = "Active offers are not deleted directly. Deactivate the offer first, then delete it.";
                        return false;
                    }
                }

                string deleteQuery = @"
                    DELETE FROM Offers
                    WHERE OfferId = @OfferId";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@OfferId", offerId);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        message = "Offer deleted successfully.";
                        return true;
                    }
                }
            }

            message = "Failed to delete offer.";
            return false;
        }

        private bool ValidateOfferInput(
            int ownerId,
            string offerTitle,
            decimal discountPercent,
            DateTime startDate,
            DateTime endDate,
            string status,
            out string message)
        {
            message = "";

            if (ownerId <= 0)
            {
                message = "Owner login is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(offerTitle))
            {
                message = "Offer title cannot be empty.";
                return false;
            }

            if (discountPercent <= 0 || discountPercent > 100)
            {
                message = "Discount percent must be greater than 0 and less than or equal to 100.";
                return false;
            }

            if (endDate.Date < startDate.Date)
            {
                message = "End date must be greater than or equal to start date.";
                return false;
            }

            if (!IsValidStatus(status))
            {
                message = "Please select a valid offer status.";
                return false;
            }

            return true;
        }

        private bool ValidateAdminOfferInput(
            int adminUserId,
            string offerTitle,
            decimal discountPercent,
            DateTime startDate,
            DateTime endDate,
            string status,
            out string message)
        {
            message = "";

            if (adminUserId <= 0)
            {
                message = "Admin or SuperAdmin login is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(offerTitle))
            {
                message = "Offer title cannot be empty.";
                return false;
            }

            if (discountPercent <= 0 || discountPercent > 100)
            {
                message = "Discount percent must be greater than 0 and less than or equal to 100.";
                return false;
            }

            if (endDate.Date < startDate.Date)
            {
                message = "End date must be greater than or equal to start date.";
                return false;
            }

            if (!IsValidStatus(status))
            {
                message = "Please select a valid offer status.";
                return false;
            }

            return true;
        }

        private bool IsOwnerCar(SqlConnection connection, int ownerId, int carId)
        {
            string query = @"
                SELECT COUNT(*)
                FROM Cars
                WHERE CarId = @CarId
                AND OwnerId = @OwnerId";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CarId", carId);
                command.Parameters.AddWithValue("@OwnerId", ownerId);

                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        private bool IsOwnerOffer(SqlConnection connection, int ownerId, int offerId)
        {
            string query = @"
                SELECT COUNT(*)
                FROM Offers
                WHERE OfferId = @OfferId
                AND CreatedByUserId = @OwnerId
                AND OfferScope IN ('Owner', 'Car')";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OfferId", offerId);
                command.Parameters.AddWithValue("@OwnerId", ownerId);

                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        private bool IsValidStatus(string status)
        {
            return status == "Active" || status == "Inactive" || status == "Expired";
        }

        private void AddOfferParameters(
            SqlCommand command,
            int ownerId,
            string offerTitle,
            string description,
            decimal discountPercent,
            DateTime startDate,
            DateTime endDate,
            int? carId,
            string offerScope,
            string status)
        {
            command.Parameters.AddWithValue("@OfferTitle", offerTitle.Trim());
            command.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description.Trim());
            command.Parameters.AddWithValue("@DiscountPercent", discountPercent);
            command.Parameters.AddWithValue("@StartDate", startDate.Date);
            command.Parameters.AddWithValue("@EndDate", endDate.Date);
            command.Parameters.AddWithValue("@OfferScope", offerScope);
            command.Parameters.AddWithValue("@CarId", carId.HasValue ? carId.Value : (object)DBNull.Value);
            command.Parameters.AddWithValue("@CreatedByUserId", ownerId);
            command.Parameters.AddWithValue("@Status", status);
        }
    }
}
