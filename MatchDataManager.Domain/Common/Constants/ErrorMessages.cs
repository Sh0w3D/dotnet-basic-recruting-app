namespace MatchDataManager.Domain.Common.Constants;

public static class ErrorMessages
{
    public static class Validation
    {
        public const string BaseMessage = "One or more validation errorc occurred";
        public const string IdEmpty = "Id should not be empty";

        #region Location

        public const string LocationNameRequired = "Location name is required and should not be empty";
        public const string LocationNameLength = "Location name should be less than 255 characters";
        public const string LocationUnique = "Location with that name already exist";
        public const string LocationCityRequired = "City name is required and should not be empty";
        public const string LocationCityLength = "City should be less than 55 characters";

        #endregion

        #region Team

        public const string TeamNameRequired = "Team name is required and should not be empty";
        public const string TeamNameLength = "Team name should be less than 255 characters";
        public const string TeamNameUnique = "Team with that name already exist";
        public const string TeamCoachNameLength = "Coach name should be less than 55 characters";

        #endregion
    }

    public static class SharedExceptions
    {
        public const string SharedExceptionMessage = "An error occurred while processing request";
        public const string SharedUnexpectedErrorMessage = "An unexpected error occurred.";
        public const string DatabaseSaveMessage = "An error occurred while saving data to database";
        public const string NotFoundMessage = "Requested data was not found";
        public const string DatabaseInitializeErrorMessage = "An error occurred while initializing database";
    }
}