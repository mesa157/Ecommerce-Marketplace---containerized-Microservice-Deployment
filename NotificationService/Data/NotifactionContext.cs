﻿using Microsoft.EntityFrameworkCore;
using NotificationService.Model;

namespace NotificationService.Data
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options) { }

        public DbSet<Notification> Notifications { get; set; }
    }
}
