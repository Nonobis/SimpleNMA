# SimpleNMA
Simple Nuget to send notification to NotifyMyAndroid.

Requirements:
    .Net Framework 4.0
    Account on http://www.notifymyandroid.com

Features:
- Check API Key is valid
- Send Notification
    
Installation:
The easiest way to get started is:

Install-Package SimpleNMA

Sample:

Check your APIKey :
  try
  {
      NMAManager.Instance.ApiKey = ""; // Set Invalid API
      var bResult = NMAManager.Instance.CheckApiKey();
      if (bResult)
      {
          Console.WriteLine("API OK");
      }
  }
  catch (Exception ex)
  {
      Console.WriteLine(ex.Message);
  }

Send a notification:

try
{
    NMAManager.Instance.ApiKey = ""; // Set Valid API
    NMAManager.Instance.ApplicationName = ""; // Set ApplicationName
    var oNotif = new NMANotification("Test", "Test Notification", PriorityNotification.Normal);
    var bResult = NMAManager.Instance.Send(oNotif);
    if (bResult)
    {
        Console.WriteLine("Notification Send with Success");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
