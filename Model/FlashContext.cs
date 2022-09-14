using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChatApp.Model
{
    public partial class FlashContext : DbContext
    {
        public FlashContext()
        {
        }

        public FlashContext(DbContextOptions<FlashContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Flash;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.ContactName).IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK__Contacts__OwnerI__4BAC3F29");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MessageBody).IsUnicode(false);

                entity.Property(e => e.SentTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__Messages__Receiv__4F7CD00D");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Messages__Sender__4E88ABD4");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FullName).IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.MailId).IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
