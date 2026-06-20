using DCOps.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DCOps.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<AssetDependency> AssetDependencies => Set<AssetDependency>();
    public DbSet<MaintenancePlan> MaintenancePlans => Set<MaintenancePlan>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<EnvironmentalReading> EnvironmentalReadings => Set<EnvironmentalReading>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<SlaDefinition> SlaDefinitions => Set<SlaDefinition>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<AutomationWorkflow> AutomationWorkflows => Set<AutomationWorkflow>();
    public DbSet<AutomationStep> AutomationSteps => Set<AutomationStep>();
    public DbSet<AutomationConnector> AutomationConnectors => Set<AutomationConnector>();
    public DbSet<AutomationJob> AutomationJobs => Set<AutomationJob>();
    public DbSet<AutomationJobLog> AutomationJobLogs => Set<AutomationJobLog>();
    public DbSet<AutomationApproval> AutomationApprovals => Set<AutomationApproval>();
    public DbSet<AutomationSchedule> AutomationSchedules => Set<AutomationSchedule>();
    public DbSet<AutomationRollbackPlan> AutomationRollbackPlans => Set<AutomationRollbackPlan>();
    public DbSet<AutomationVariable> AutomationVariables => Set<AutomationVariable>();
    public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
    public DbSet<LocalizationKey> LocalizationKeys => Set<LocalizationKey>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username).IsUnique();

        // AssetDependency - avoid multiple cascade paths
        modelBuilder.Entity<AssetDependency>()
            .HasOne(ad => ad.Asset)
            .WithMany()
            .HasForeignKey(ad => ad.AssetId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AssetDependency>()
            .HasOne(ad => ad.DependentAsset)
            .WithMany()
            .HasForeignKey(ad => ad.DependentAssetId)
            .OnDelete(DeleteBehavior.Restrict);

        // AutomationWorkflow -> AutomationStep
        modelBuilder.Entity<AutomationStep>()
            .HasOne(s => s.AutomationWorkflow)
            .WithMany(w => w.Steps)
            .HasForeignKey(s => s.AutomationWorkflowId)
            .OnDelete(DeleteBehavior.Cascade);

        // AutomationWorkflow -> AutomationJob
        modelBuilder.Entity<AutomationJob>()
            .HasOne(j => j.AutomationWorkflow)
            .WithMany(w => w.Jobs)
            .HasForeignKey(j => j.AutomationWorkflowId)
            .OnDelete(DeleteBehavior.Cascade);

        // AutomationJob -> AutomationJobLog
        modelBuilder.Entity<AutomationJobLog>()
            .HasOne(l => l.AutomationJob)
            .WithMany(j => j.Logs)
            .HasForeignKey(l => l.AutomationJobId)
            .OnDelete(DeleteBehavior.Cascade);

        // AutomationJob -> AutomationApproval
        modelBuilder.Entity<AutomationApproval>()
            .HasOne(a => a.AutomationJob)
            .WithMany(j => j.Approvals)
            .HasForeignKey(a => a.AutomationJobId)
            .OnDelete(DeleteBehavior.Cascade);

        // AutomationWorkflow -> AutomationSchedule
        modelBuilder.Entity<AutomationSchedule>()
            .HasOne(s => s.AutomationWorkflow)
            .WithMany(w => w.Schedules)
            .HasForeignKey(s => s.AutomationWorkflowId)
            .OnDelete(DeleteBehavior.Cascade);

        // AutomationWorkflow -> AutomationRollbackPlan
        modelBuilder.Entity<AutomationRollbackPlan>()
            .HasOne(r => r.AutomationWorkflow)
            .WithMany(w => w.RollbackPlans)
            .HasForeignKey(r => r.AutomationWorkflowId)
            .OnDelete(DeleteBehavior.Cascade);

        // LocalizationKey - unique key name
        modelBuilder.Entity<LocalizationKey>()
            .HasIndex(k => k.KeyName).IsUnique();
    }
}
