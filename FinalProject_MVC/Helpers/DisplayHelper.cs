namespace FinalProject_MVC.Helpers
{
    public static class DisplayHelper
    {
        public static string RequestStatusTitle(string key)
        {
            return key switch
            {
                "Pending" => "در انتظار",
                "CurrentlyViewing" => "در حال مشاهده و برررسی",
                "Interview" => "آماده برای محاسبه",
                "Success" => "پذیرفته شد",
                "Fail" => "پذیرفته نشد",
                _ => key
            };
        }
    }
}
