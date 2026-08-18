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

        public static string FormatDate(DateTime? date)
        {
            if (date == null || date.Value == default || date.Value == DateTime.MinValue)
                return "-";

            return date.Value.ToString("yyyy/MM/dd");
        }

        public static string SettingTitle(string key)
        {
            return key switch
            {
                "SiteName" => "نام سایت",
                "SiteDescription" => "توضیح سایت",
                "ContactEmail" => "ایمیل پشتیبانی",
                "IsRegistrationOpen" => "ثبت‌نام باز است؟ (true / false)",
                "MaxActiveAdsPerEmployer" => "حداکثر آگهی فعال هر کارفرما",
                _ => key
            };
        }
    }
}
