namespace TennisApp.Common.Validations
{
    public class ValidationConstants
    {
        //Common
        public const string BirthDateFormat = "dd-MM-YYYY";
        public const int FullNameMinLength = 3;
        public const int FullNameMaxLength = 50;

        //Booking
        public const string BookingDateFormat = "dd/MM HH:mm";

        //Court
        public const int CourtNameMinLength = 1;
        public const int CourtNameMaxLength = 50;
    }
}
