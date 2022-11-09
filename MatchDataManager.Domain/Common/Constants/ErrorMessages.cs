namespace MatchDataManager.Domain.Common.Constants;

public static class ErrorMessages
{
    public static class Validation
    {
        public const string BaseMessage = "One or more validation errorc occurred";

        #region Location
        public const string LocationNameRequired = "Location name is required";
        public const string LocationNameLength = "Location name should be less than 255 characters";
        public const string LocationUnique = "Location with that name already exist";
        public const string LocationCityRequired = "City name is required";
        public const string LocationCityLength = "City should be less than 55 characters";
        #endregion
        
        #region Team
        public const string TeamNameRequired = "Team name is required";
        public const string TeamNameLength = "Team name should be less than 255 characters";
        public const string TeamCoachNameLength = "Coach name should be less than 55 characters";
        public const string TeamCoachNameUnique = "Coach with that name already exist";
        #endregion
    }
    public static class SharedExceptions
    {
        public const string SharedExceptionMessage = "An error occurred while processing request";
        public const string SharedUnexpectedErrorMessage = "An unexpected error occurred.";
        public const string DatabaseSaveMessage = "An error occurred while saving data to database";
        public const string NotFoundMessage = "Requested data was not found";
    }
}