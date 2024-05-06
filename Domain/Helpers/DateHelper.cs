namespace Domain.Helpers
{
    public class DateHelper
    {
        public bool IsFutureDate(DateTime date)
        {
            return date.Date >= DateTime.Today.Date;
        }

        public int DateDifferenceDays(DateTime date)
        {
            return Math.Abs((DateTime.Today.Date - date.Date).Days);
        }
    }
}
