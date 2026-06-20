using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DCOps.Web.Models.Entities;

public class User
{
    public int Id { get; set; }
    [Required, MaxLength(100)] public string Username { get; set; } = "";
    [Required] public string PasswordHash { get; set; } = "";
    [MaxLength(200)] public string? Email { get; set; }
    [MaxLength(200)] public string? FullName { get; set; }
    [MaxLength(50)] public string Role { get; set; } = "viewer";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }
}

public class Asset
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string AssetCode { get; set; } = "";
    [Required, MaxLength(200)] public string Name { get; set; } = "";
    [MaxLength(100)] public string? AssetType { get; set; }
    [MaxLength(100)] public string? Category { get; set; }
    [MaxLength(50)] public string Status { get; set; } = "active";
    [MaxLength(200)] public string? Location { get; set; }
    [MaxLength(100)] public string? SerialNumber { get; set; }
    [MaxLength(100)] public string? Manufacturer { get; set; }
    [MaxLength(100)] public string? Model { get; set; }
    [MaxLength(50)] public string? IpAddress { get; set; }
    [MaxLength(100)] public string? Owner { get; set; }
    [MaxLength(100)] public string? Department { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public DateTime? WarrantyExpiry { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class AssetDependency
{
    public int Id { get; set; }
    public int AssetId { get; set; }
    public int DependentAssetId { get; set; }
    [MaxLength(100)] public string? DependencyType { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AssetId))] public Asset? Asset { get; set; }
    [ForeignKey(nameof(DependentAssetId))] public Asset? DependentAsset { get; set; }
}

public class MaintenancePlan
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string PlanCode { get; set; } = "";
    [Required, MaxLength(200)] public string Name { get; set; } = "";
    public string? Description { get; set; }
    [MaxLength(50)] public string? Frequency { get; set; }
    public int? AssetId { get; set; }
    [MaxLength(100)] public string? AssignedTo { get; set; }
    [MaxLength(50)] public string Status { get; set; } = "active";
    public DateTime? NextScheduled { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AssetId))] public Asset? Asset { get; set; }
}

public class WorkOrder
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string OrderNo { get; set; } = "";
    [Required, MaxLength(200)] public string Title { get; set; } = "";
    public string? Description { get; set; }
    [MaxLength(50)] public string? Type { get; set; }
    [MaxLength(50)] public string Status { get; set; } = "open";
    [MaxLength(20)] public string Priority { get; set; } = "medium";
    public int? AssetId { get; set; }
    [MaxLength(100)] public string? AssignedTo { get; set; }
    [MaxLength(100)] public string? RequestedBy { get; set; }
    public DateTime? ScheduledAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AssetId))] public Asset? Asset { get; set; }
}

public class EnvironmentalReading
{
    public int Id { get; set; }
    [MaxLength(50)] public string? SensorId { get; set; }
    [MaxLength(100)] public string? SensorName { get; set; }
    [MaxLength(100)] public string? Location { get; set; }
    public decimal? Temperature { get; set; }
    public decimal? Humidity { get; set; }
    [MaxLength(50)] public string? ReadingType { get; set; }
    public decimal? Value { get; set; }
    [MaxLength(20)] public string? Unit { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
}

public class Contract
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string ContractNo { get; set; } = "";
    [Required, MaxLength(200)] public string Title { get; set; } = "";
    [MaxLength(100)] public string? ContractType { get; set; }
    [MaxLength(200)] public string? Vendor { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal? Value { get; set; }
    [MaxLength(50)] public string Status { get; set; } = "active";
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class SlaDefinition
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string SlaCode { get; set; } = "";
    [Required, MaxLength(200)] public string Name { get; set; } = "";
    public int ResponseTimeHours { get; set; }
    public int ResolutionTimeHours { get; set; }
    [MaxLength(20)] public string Priority { get; set; } = "medium";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Notification
{
    public int Id { get; set; }
    [Required, MaxLength(200)] public string Title { get; set; } = "";
    public string? Message { get; set; }
    [MaxLength(50)] public string? NotificationType { get; set; }
    [MaxLength(20)] public string Severity { get; set; } = "info";
    public bool IsRead { get; set; } = false;
    public int? UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReadAt { get; set; }

    [ForeignKey(nameof(UserId))] public User? User { get; set; }
}

public class AuditLog
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    [MaxLength(100)] public string? Username { get; set; }
    [MaxLength(100)] public string? Action { get; set; }
    [MaxLength(100)] public string? EntityType { get; set; }
    [MaxLength(50)] public string? EntityId { get; set; }
    public string? Details { get; set; }
    [MaxLength(50)] public string? IpAddress { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class AutomationWorkflow
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string WorkflowCode { get; set; } = "";
    [MaxLength(200)] public string? NameAr { get; set; }
    [MaxLength(200)] public string? NameEn { get; set; }
    [MaxLength(100)] public string? Category { get; set; }
    [MaxLength(50)] public string? TriggerType { get; set; }
    [MaxLength(50)] public string? RiskLevel { get; set; }
    public bool RequiresApproval { get; set; } = false;
    [MaxLength(50)] public string Status { get; set; } = "active";
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<AutomationStep> Steps { get; set; } = new List<AutomationStep>();
    public ICollection<AutomationJob> Jobs { get; set; } = new List<AutomationJob>();
    public ICollection<AutomationSchedule> Schedules { get; set; } = new List<AutomationSchedule>();
    public ICollection<AutomationRollbackPlan> RollbackPlans { get; set; } = new List<AutomationRollbackPlan>();
}

public class AutomationStep
{
    public int Id { get; set; }
    public int AutomationWorkflowId { get; set; }
    public int StepNo { get; set; }
    [MaxLength(200)] public string? StepName { get; set; }
    [MaxLength(50)] public string? StepType { get; set; }
    [MaxLength(50)] public string? ConnectorType { get; set; }
    [MaxLength(200)] public string? ActionName { get; set; }
    public string? ParametersJson { get; set; }
    public bool StopOnFailure { get; set; } = true;
    public int TimeoutSeconds { get; set; } = 300;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AutomationWorkflowId))] public AutomationWorkflow? AutomationWorkflow { get; set; }
}

public class AutomationConnector
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string ConnectorCode { get; set; } = "";
    [Required, MaxLength(200)] public string Name { get; set; } = "";
    [MaxLength(50)] public string? ConnectorType { get; set; }
    [MaxLength(500)] public string? BaseUrl { get; set; }
    [MaxLength(50)] public string? AuthType { get; set; }
    [MaxLength(50)] public string Status { get; set; } = "active";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class AutomationJob
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string JobNo { get; set; } = "";
    public int AutomationWorkflowId { get; set; }
    [MaxLength(50)] public string Status { get; set; } = "pending";
    [Column(TypeName = "decimal(5,2)")] public decimal ProgressPct { get; set; } = 0;
    [MaxLength(100)] public string? RequestedBy { get; set; }
    [MaxLength(50)] public string? SourceType { get; set; }
    [MaxLength(50)] public string? SourceId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    [ForeignKey(nameof(AutomationWorkflowId))] public AutomationWorkflow? AutomationWorkflow { get; set; }
    public ICollection<AutomationJobLog> Logs { get; set; } = new List<AutomationJobLog>();
    public ICollection<AutomationApproval> Approvals { get; set; } = new List<AutomationApproval>();
}

public class AutomationJobLog
{
    public int Id { get; set; }
    public int AutomationJobId { get; set; }
    public int? AutomationStepId { get; set; }
    [MaxLength(20)] public string LogLevel { get; set; } = "info";
    public string? Message { get; set; }
    [MaxLength(50)] public string? Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AutomationJobId))] public AutomationJob? AutomationJob { get; set; }
}

public class AutomationApproval
{
    public int Id { get; set; }
    public int AutomationJobId { get; set; }
    [MaxLength(50)] public string? ApproverRole { get; set; }
    [MaxLength(50)] public string Status { get; set; } = "pending";
    [MaxLength(100)] public string? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AutomationJobId))] public AutomationJob? AutomationJob { get; set; }
}

public class AutomationSchedule
{
    public int Id { get; set; }
    public int AutomationWorkflowId { get; set; }
    [MaxLength(200)] public string? ScheduleName { get; set; }
    [MaxLength(50)] public string? Frequency { get; set; }
    public DateTime? NextRunAt { get; set; }
    [MaxLength(50)] public string Status { get; set; } = "active";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AutomationWorkflowId))] public AutomationWorkflow? AutomationWorkflow { get; set; }
}

public class AutomationRollbackPlan
{
    public int Id { get; set; }
    public int AutomationWorkflowId { get; set; }
    public int StepNo { get; set; }
    [MaxLength(200)] public string? RollbackAction { get; set; }
    public string? ParametersJson { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AutomationWorkflowId))] public AutomationWorkflow? AutomationWorkflow { get; set; }
}

public class AutomationVariable
{
    public int Id { get; set; }
    public int? AutomationWorkflowId { get; set; }
    [Required, MaxLength(100)] public string VariableKey { get; set; } = "";
    public string? VariableValue { get; set; }
    public bool IsSecret { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(AutomationWorkflowId))] public AutomationWorkflow? AutomationWorkflow { get; set; }
}

public class ServiceRequest
{
    public int Id { get; set; }
    [Required, MaxLength(50)] public string RequestNo { get; set; } = "";
    [Required, MaxLength(200)] public string Title { get; set; } = "";
    public string? Description { get; set; }
    [MaxLength(50)] public string? RequestType { get; set; }
    [MaxLength(50)] public string Status { get; set; } = "open";
    [MaxLength(20)] public string Priority { get; set; } = "medium";
    [MaxLength(100)] public string? RequestedBy { get; set; }
    [MaxLength(100)] public string? AssignedTo { get; set; }
    public int? AssetId { get; set; }
    public int? SlaId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ClosedAt { get; set; }

    [ForeignKey(nameof(AssetId))] public Asset? Asset { get; set; }
    [ForeignKey(nameof(SlaId))] public SlaDefinition? Sla { get; set; }
}

public class LocalizationKey
{
    public int Id { get; set; }
    [Required, MaxLength(200)] public string KeyName { get; set; } = "";
    [MaxLength(100)] public string? Module { get; set; }
    public string? ValueAr { get; set; }
    public string? ValueEn { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
