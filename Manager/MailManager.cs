namespace webApp.Manager
{
    public class MailSender
    {   
        // email list
        public static string work_mail = "wrok@mail.com";
        public static string home_mail = "home@mail.com";
        public static string personal_mail = "ali@mail.com";
        public static string web_mail = "web@domain.com";
        // password list
        private static string work_mail_pw = "********"; // read from database or file encrypted
        private static string home_mail_pw = "********";
        private static string personal_mail_pw = "********";
        private static string web_mail_pw = "********";

        public enum state
        {
            work_mail,
            home_mail,
            personal_mail,
        }
        private void doSendMail(string from, string to, string message)
        {
            // lib to send mail using with
        }
        public void sendMail(state? s, string to, string message)
        {
            switch (s)
            {
                case state.work_mail:
                    doSendMail(work_mail,to, message);
                    break;
                case state.home_mail:
                    doSendMail(home_mail,to, message);
                    break;
                case state.personal_mail:
                    doSendMail(personal_mail,to, message);
                    break;
                default:
                    doSendMail(personal_mail, to, message);
                    break;
            }

        }

    }
}
