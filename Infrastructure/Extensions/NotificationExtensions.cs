using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IndianArmyWeb.Infrastructure.Extensions
{
    public static class NotificationExtensions
    {
        private static IDictionary<String, String> NotificationKey = new Dictionary<String, String>
        {
            { "Error",      "App.Notifications.Error" },
            { "Warning",    "App.Notifications.Warning" },
            { "Success",    "App.Notifications.Success" },
            { "Info",       "App.Notifications.Info" }
        };

        public static void AddNotification(this ControllerBase controller, String message, String notificationType)
        {
            string NotificationKey = getNotificationKeyByType(notificationType);
            ICollection<String> messages = controller.TempData[NotificationKey] as ICollection<String>;

            if (messages == null)
            {
                controller.TempData[NotificationKey] = (messages = new HashSet<String>());
            }

            messages.Add(message);
        }

        public static IEnumerable<String> GetNotifications(this HtmlHelper htmlHelper, String notificationType)
        {
            string NotificationKey = getNotificationKeyByType(notificationType);
            return htmlHelper.ViewContext.Controller.TempData[NotificationKey] as ICollection<String> ?? null;
        }

        private static string getNotificationKeyByType(string notificationType)
        {
            try
            {
                return NotificationKey[notificationType];
            }
            catch (IndexOutOfRangeException e)
            {
                ArgumentException exception = new ArgumentException("Key is invalid", "notificationType", e);
                throw exception;
            }
        }
        public static void SetPromptNotification(Controller controller, string NotifyType)
        {
            controller.TempData["PromptNotification"] = NotifyType;
            //controller.ViewBag.PromptNotification = NotifyType;
        }


        //------------------Start---------------------//

        public static void SetPromptNotification(Controller controller, bool success, string message)
        {
            PromptNotification promptNotification = new PromptNotification
            {
                NotificationSuccess = success,
                NotificationMessage = message
            };
            controller.TempData["PromptNotification"] = promptNotification;
        }

        public static PromptNotification GetPromptNotification(Controller controller)
        {
            try
            {
                PromptNotification promptNotification = controller.TempData["PromptNotification"] as PromptNotification;
                return promptNotification;
            }
            catch (Exception ex)
            {
                // log error here

                return null;
            }
        }

        public static void RemovePromptNotification(Controller controller)
        {
            controller.TempData.Remove("PromptNotification");
        }
        //------------------End----------------------//
    }

    public static class NotificationType
    {
        public const string INSERT = "Insert";
        public const string UPDATE = "Update";
        public const string DELETE = "Delete";

        public const string ERROR = "Error";
        public const string WARNING = "Warning";
        public const string SUCCESS = "Success";
        public const string INFO = "Info";
    }

    public class PromptNotification //Notification Object
    {
        public bool NotificationSuccess { get; set; }
        public string NotificationMessage { get; set; }
    }
}