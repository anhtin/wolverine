﻿using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core.Migrations;
using Wolverine.Persistence.Durability;
using Wolverine.Persistence.Sagas;
using Wolverine.RDBMS;
using Wolverine.SqlServer.Persistence;
using Wolverine.SqlServer.Util;

namespace Wolverine.SqlServer;

/// <summary>
///     Activates the Sql Server backed message persistence
/// </summary>
internal class SqlServerBackedPersistence : IWolverineExtension
{
    public SqlServerSettings Settings { get; } = new();

    public void Configure(WolverineOptions options)
    {
        options.Services.AddSingleton(Settings);

        options.Services.AddTransient<IMessageStore, SqlServerMessageMessageStore>();
        options.Services.AddSingleton(s => (IDatabase)s.GetRequiredService<IMessageStore>());

        options.Node.CodeGeneration.Sources.Add(new DatabaseBackedPersistenceMarker());

        options.Services.For<SqlConnection>().Use<SqlConnection>();

        options.Services.Add(new ServiceDescriptor(typeof(SqlConnection),
            new SqlConnectionInstance(typeof(SqlConnection))));
        options.Services.Add(new ServiceDescriptor(typeof(DbConnection),
            new SqlConnectionInstance(typeof(DbConnection))));

        // Don't overwrite the EF Core transaction support if it's there
        options.Node.CodeGeneration.SetTransactionsIfNone(new SqlServerTransactionFrameProvider());
    }
}