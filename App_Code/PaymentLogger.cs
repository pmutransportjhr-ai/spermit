using System;
using System.IO;
using System.Text;
using System.Web;

/// <summary>
/// Thread-safe payment logger with daily rolling files.
/// Logs to App_Data/Logs/Payments/ directory.
/// NEVER throws exceptions — logging failures must not break payment processing.
/// </summary>
public static class PaymentLogger
{
    private static readonly object _lock = new object();

    /// <summary>
    /// Generate a unique verification ID for correlating all events in one verification attempt.
    /// Format: PV-yyyyMMdd-XXXXXXXX
    /// </summary>
    public static string GenerateVerificationId()
    {
        return "PV-" + DateTime.Now.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
    }

    // ─── Public logging methods ────────────────────────────────────────────

    public static void Info(string verificationId, string applicationNumber, string depttranid,
        string stage, string message, string appId = "", string status = "", string operation = "")
    {
        WriteLog("INFO", verificationId, applicationNumber, depttranid, stage, message, appId, status, operation);
    }

    public static void Warning(string verificationId, string applicationNumber, string depttranid,
        string stage, string message, string appId = "", string status = "", string operation = "")
    {
        WriteLog("WARN", verificationId, applicationNumber, depttranid, stage, message, appId, status, operation);
    }

    public static void Error(string verificationId, string applicationNumber, string depttranid,
        string stage, string message, string appId = "", string status = "", string operation = "")
    {
        WriteLog("ERROR", verificationId, applicationNumber, depttranid, stage, message, appId, status, operation);
    }

    public static void GatewayRequest(string verificationId, string applicationNumber, string depttranid,
        string message, string appId = "", string operation = "")
    {
        WriteLog("GATEWAY_REQ", verificationId, applicationNumber, depttranid, "JEGRAS_REQUEST_SENT", message, appId, "", operation);
    }

    public static void GatewayResponse(string verificationId, string applicationNumber, string depttranid,
        string message, string returnedStatus = "", string appId = "", string operation = "")
    {
        WriteLog("GATEWAY_RES", verificationId, applicationNumber, depttranid, "JEGRAS_RESPONSE_RECEIVED", message, appId, returnedStatus, operation);
    }

    public static void DatabaseUpdate(string verificationId, string applicationNumber, string depttranid,
        string stage, string message, string appId = "", string fromStatus = "", string toStatus = "", string operation = "")
    {
        string statusInfo = "";
        if (!string.IsNullOrEmpty(fromStatus) || !string.IsNullOrEmpty(toStatus))
            statusInfo = " [" + fromStatus + " -> " + toStatus + "]";
        WriteLog("DB_UPDATE", verificationId, applicationNumber, depttranid, stage, message + statusInfo, appId, toStatus, operation);
    }

    // ─── Pre-verification context log ────────────────────────────────────

    /// <summary>
    /// Logs the full context before calling JEGRAS. 
    /// Does NOT log encryption key, security code, or connection string.
    /// </summary>
    public static void LogVerificationContext(
        string verificationId,
        string applicationNumber,
        string appId,
        string depttranid,
        string currentDbStatus,
        string operation,
        string amount,
        string dbGrn)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("VERIFICATION_CONTEXT | ");
        sb.Append("ApplicationNumber=").Append(applicationNumber ?? "").Append(" | ");
        sb.Append("APP_ID=").Append(appId ?? "").Append(" | ");
        sb.Append("DEPTTRANID=").Append(depttranid ?? "").Append(" | ");
        sb.Append("CurrentDBStatus=").Append(currentDbStatus ?? "").Append(" | ");
        sb.Append("Operation=").Append(operation ?? "").Append(" | ");
        sb.Append("Amount=").Append(amount ?? "").Append(" | ");
        sb.Append("DB_GRN=").Append(dbGrn ?? "");

        WriteLog("INFO", verificationId, applicationNumber, depttranid,
            "DOUBLE_VERIFICATION_REQUEST_CREATED", sb.ToString(), appId, currentDbStatus, operation);
    }

    // ─── Core write method ───────────────────────────────────────────────

    private static void WriteLog(string level, string verificationId, string applicationNumber,
        string depttranid, string stage, string message, string appId, string status, string operation)
    {
        try
        {
            string basePath = GetLogDirectory();
            string fileName = "Payment_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string filePath = Path.Combine(basePath, fileName);

            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            sb.Append(" | ").Append(level.PadRight(12));
            sb.Append(" | VID=").Append(verificationId ?? "-");
            sb.Append(" | APP=").Append(applicationNumber ?? "-");
            sb.Append(" | DEPTTRANID=").Append(depttranid ?? "-");
            if (!string.IsNullOrEmpty(appId))
                sb.Append(" | APP_ID=").Append(appId);
            if (!string.IsNullOrEmpty(status))
                sb.Append(" | STATUS=").Append(status);
            if (!string.IsNullOrEmpty(operation))
                sb.Append(" | OP=").Append(operation);
            sb.Append(" | ").Append(stage ?? "-");
            sb.Append(" | ").Append(message ?? "");
            sb.AppendLine();

            lock (_lock)
            {
                File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
            }
        }
        catch
        {
            // NEVER let logging break payment processing
        }
    }

    private static string GetLogDirectory()
    {
        string basePath;
        try
        {
            basePath = HttpContext.Current.Server.MapPath("~/App_Data/Logs/Payments");
        }
        catch
        {
            basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Logs", "Payments");
        }

        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }
        return basePath;
    }
}
