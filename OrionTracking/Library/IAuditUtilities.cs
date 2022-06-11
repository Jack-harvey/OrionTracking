using OrionTracking.Areas.Identity.Data;

namespace OrionTracking.Library
{
    public interface IAuditUtilities
    {
        public Task WriteAuditData(string columnName, int rowId, string oldValue, string newValue, string userGuid, string note);
    }
}
