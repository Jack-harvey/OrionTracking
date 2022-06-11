using Microsoft.EntityFrameworkCore;
using OrionTracking.Areas.Identity.Data;
using OrionTracking.Data;
using OrionTracking.Models;

namespace OrionTracking.Library
{
    public class AuditUtilities : IAuditUtilities
    {
        private readonly OrionContext _context;
        private readonly ILogger<AuditUtilities> _logger;

        public AuditUtilities(ILogger<AuditUtilities> logger, OrionContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task WriteAuditData(string columnName, int rowId, string oldValue, string newValue, string userGuid, string note)
        {
            var audit = new Audit
            {
                ColumnName = columnName,
                RowId = rowId,
                OldValue = oldValue,
                NewValue = newValue,
                AspNetUserId = userGuid,
                Note = note,
                Timestamp = DateTime.UtcNow
            };

            _context.Add(audit);
            await _context.SaveChangesAsync();
        }
    }
}
