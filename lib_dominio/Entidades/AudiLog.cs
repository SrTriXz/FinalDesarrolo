// Path: lib_dominio/Entidades/AuditLog.cs
using System;
using System.ComponentModel.DataAnnotations; // Para [Key]

namespace lib_dominio.Entidades
{
    public class AuditLog
    {
        [Key] public int Id { get; set; }
        public string? UserId { get; set; } 
        public string? EntityType { get; set; } 
        public string? EntityKey { get; set; } 
        public string? ActionType { get; set; } 
        public DateTime Timestamp { get; set; } 
        public string? OldValues { get; set; } 
        public string? NewValues { get; set; } 
        public string? ChangedColumns { get; set; }
    }
}