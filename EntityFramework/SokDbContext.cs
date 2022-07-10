using FeederSokML.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FeederSokML.EntityFramework
{
    public class SokDbContext : DbContext
    {
        public SokDbContext(DbContextOptions<SokDbContext> options) : base(options)
        {

        }

        public DbSet<Procesy> Procesy { get; set; }
        public DbSet<ProcesRezultML> ProcesRezultML { get; set; }
        public DbSet<DocumentForClassification> GetMLResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Procesy>(entity =>
            {
                entity.Property(x => x.DataOdlozone).HasColumnName("Data_odlozone");
                entity.Property(x => x.Val1).HasColumnName("VAL_1");
                entity.Property(x => x.Val2).HasColumnName("VAL_2");
                entity.Property(x => x.Val3).HasColumnName("VAL_3");
                entity.Property(x => x.Val4).HasColumnName("VAL_4");
                entity.Property(x => x.Val5).HasColumnName("VAL_5");
                entity.Property(x => x.Val6).HasColumnName("VAL_6");
                entity.Property(x => x.Val7).HasColumnName("VAL_7");
                entity.Property(x => x.Val8).HasColumnName("VAL_8");
                entity.Property(x => x.Val9).HasColumnName("VAL_9");
                entity.Property(x => x.Val10).HasColumnName("VAL_10");
                entity.Property(x => x.Val11).HasColumnName("VAL_11");
                entity.Property(x => x.Val12).HasColumnName("VAL_12");
                entity.Property(x => x.Val13).HasColumnName("VAL_13");
                entity.Property(x => x.Val14).HasColumnName("VAL_14");
                entity.Property(x => x.Val15).HasColumnName("VAL_15");
                entity.Property(x => x.Val16).HasColumnName("VAL_16");
                entity.Property(x => x.Val17).HasColumnName("VAL_17");
                entity.Property(x => x.Val18).HasColumnName("VAL_18");
                entity.Property(x => x.Val19).HasColumnName("VAL_19");
                entity.Property(x => x.Val20).HasColumnName("VAL_20");
                entity.Property(x => x.Val21).HasColumnName("VAL_21");
                entity.Property(x => x.Val22).HasColumnName("VAL_22");
                entity.Property(x => x.Val23).HasColumnName("VAL_23");
                entity.Property(x => x.Val24).HasColumnName("VAL_24");
                entity.Property(x => x.Val25).HasColumnName("VAL_25");
            });

            modelBuilder.Entity<ProcesRezultML>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("PK_proces_RezultML");
                entity.Property(x => x.IddictProcesClassificationML).HasColumnName("iddict_Proces_ClassificationML");
                entity.Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<DocumentForClassification>(entity =>
            {
                entity.HasNoKey();
                entity.Property(x => x.LinkTiff).HasColumnName("linkTif");
                entity.Property(x => x.IdDictProcesClassificationML).HasColumnName("iddict_Proces_ClassificationML");
            });
        }
    }
}
