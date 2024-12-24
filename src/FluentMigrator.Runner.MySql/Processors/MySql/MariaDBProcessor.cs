#region License
// Copyright (c) 2024, Fluent Migrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using FluentMigrator.Expressions;
using FluentMigrator.Runner.Generators.MySql;
using FluentMigrator.Runner.Helpers;
using FluentMigrator.Runner.Initialization;

using JetBrains.Annotations;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluentMigrator.Runner.Processors.MySql
{
    public class MariaDBProcessor : MySqlProcessor
    {
        [NotNull] private readonly IConnectionStringAccessor _connectionStringAccessor;
        private const string TABLE_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}'";
        private const string COLUMN_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}' AND COLUMN_NAME = '{1}'";
        private const string CONSTRAINT_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}' AND CONSTRAINT_NAME = '{1}'";
        private const string INDEX_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS WHERE TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}' AND INDEX_NAME = '{1}'";
        private const string SEQUENCES_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'SEQUENCE' AND TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}'";
        private const string DEFAULT_VALUE_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}' AND COLUMN_NAME = '{1}' AND COLUMN_DEFAULT LIKE '{2}'";
        private const string CREATE_DATABASE = "CREATE DATABASE IF NOT EXISTS {0}";
        private const string DROP_DATABASE = "DROP DATABASE IF EXISTS {0}";
        private const string DATABASE_EXISTS = "SHOW DATABASES LIKE {0}";

        private readonly MariaDBQuoter _quoter = new MariaDBQuoter();
        private readonly DbProviderFactory _factory;

        public MariaDBProcessor(
            [NotNull] MySqlDbFactory factory,
            [NotNull] MariaDBGenerator generator,
            [NotNull] ILogger<MariaDBProcessor> logger,
            [NotNull] IOptionsSnapshot<ProcessorOptions> options,
            [NotNull] IConnectionStringAccessor connectionStringAccessor) : base(
            factory,
            generator,
            logger,
            options,
            connectionStringAccessor)
        {
            _factory = factory.Factory;
            _connectionStringAccessor = connectionStringAccessor;
        }

        public override string DatabaseType => ProcessorId.MariaDB;

        public override IList<string> DatabaseTypeAliases { get; } = new List<string>();

        public override bool SchemaExists(string schemaName)
        {
            return true;
        }

        public override bool TableExists(string schemaName, string tableName)
        {
            return Exists(TABLE_EXISTS, FormatHelper.FormatSqlEscape(tableName));
        }

        public override bool ColumnExists(string schemaName, string tableName, string columnName)
        {
            return Exists(COLUMN_EXISTS, FormatHelper.FormatSqlEscape(tableName), FormatHelper.FormatSqlEscape(columnName));
        }

        public override bool ConstraintExists(string schemaName, string tableName, string constraintName)
        {
            return Exists(CONSTRAINT_EXISTS, FormatHelper.FormatSqlEscape(tableName), FormatHelper.FormatSqlEscape(constraintName));
        }

        public override bool IndexExists(string schemaName, string tableName, string indexName)
        {
            return Exists(INDEX_EXISTS, FormatHelper.FormatSqlEscape(tableName), FormatHelper.FormatSqlEscape(indexName));
        }

        public override bool SequenceExists(string schemaName, string sequenceName)
        {
            return Exists(SEQUENCES_EXISTS, FormatHelper.FormatSqlEscape(sequenceName));
        }

        public override bool DefaultValueExists(string schemaName, string tableName, string columnName, object defaultValue)
        {
            if (defaultValue == null || DBNull.Value.Equals(defaultValue)) defaultValue = "NULL";
            var defaultValueAsString = string.Format("%{0}%", FormatHelper.FormatSqlEscape(defaultValue.ToString()));
            return Exists(DEFAULT_VALUE_EXISTS, FormatHelper.FormatSqlEscape(tableName), FormatHelper.FormatSqlEscape(columnName), defaultValueAsString);
        }

        public override void Execute(string template, params object[] args)
        {
            var commandText = string.Format(template, args);

            Logger.LogSql(commandText);

            if (Options.PreviewOnly || string.IsNullOrEmpty(commandText))
            {
                return;
            }

            EnsureConnectionIsOpen();

            using (var command = CreateCommand(commandText))
            {
                command.ExecuteNonQuery();
            }
        }

        private void Execute(IDbConnection connection, string template, params object[] args)
        {
            var commandText = string.Format(template, args);

            Logger.LogSql(commandText);

            if (Options.PreviewOnly || string.IsNullOrEmpty(commandText))
            {
                return;
            }

            EnsureConnectionIsOpen(connection);

            using (var command = CreateCommand(commandText, connection, null))
            {
                command.ExecuteNonQuery();
            }
        }

        private void EnsureConnectionIsOpen(IDbConnection connection)
        {
            if (connection != null && connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        private void EnsureConnectionIsClosed(IDbConnection connection)
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        private bool Exists(IDbConnection connection, string template, params object[] args)
        {
            EnsureConnectionIsOpen(connection);

            using (var command = CreateCommand(string.Format(template, args), connection, null))
            {
                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        return reader.Read();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public override DataSet ReadTableData(string schemaName, string tableName)
        {
            return Read("SELECT * FROM {0}", _quoter.QuoteTableName(tableName, schemaName));
        }

        public override void Process(RenameColumnExpression expression)
        {
            Process(Generator.Generate(expression));
        }

        public override void CreateDatabaseIfNotExists()
        {
            var dbName = GetDatabaseName();
            IDbConnection rootConnection = null;
            try
            {
                rootConnection = CreateRootConnection();
                Execute(rootConnection, CREATE_DATABASE, _quoter.Quote(dbName));
                EnsureConnectionIsClosed(rootConnection);
            }
            catch
            {
                EnsureConnectionIsClosed(rootConnection);
                throw;
            }
        }

        public override void DropDatabaseIfExists()
        {
            var dbName = GetDatabaseName();
            IDbConnection rootConnection = null;
            try
            {
                rootConnection = CreateRootConnection();
                Execute(rootConnection, DROP_DATABASE, _quoter.Quote(dbName));
                EnsureConnectionIsClosed(rootConnection);
            }
            catch
            {
                EnsureConnectionIsClosed(rootConnection);
                throw;
            }
        }

        public override bool DatabaseExists()
        {
            var dbName = GetDatabaseName();
            IDbConnection rootConnection = null;
            try
            {
                rootConnection = CreateRootConnection();
                var result = Exists(rootConnection, DATABASE_EXISTS, _quoter.QuoteValue(dbName));
                EnsureConnectionIsClosed(rootConnection);
                return result;
            }
            catch
            {
                EnsureConnectionIsClosed(rootConnection);
                throw;
            }
        }

        private DbConnection CreateRootConnection()
        {
            var conn = _factory.CreateConnection();

            if (conn == null)
            {
                throw new InvalidOperationException("Could not create root connection");
            }

            conn.ConnectionString = GetRootConnectionString();

            return conn;
        }

        private string GetRootConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionStringAccessor.ConnectionString))
            {
                throw new InvalidOperationException("Missing connection string");
            }

            var csBuilder = new DbConnectionStringBuilder();
            csBuilder.ConnectionString = _connectionStringAccessor.ConnectionString;
            csBuilder.Remove("Database");
            return csBuilder.ToString();
        }

        private string GetDatabaseName()
        {
            if (string.IsNullOrEmpty(_connectionStringAccessor.ConnectionString))
            {
                return null;
            }

            var csBuilder = new DbConnectionStringBuilder();
            csBuilder.ConnectionString = _connectionStringAccessor.ConnectionString;
            var dbName = csBuilder["Database"]?.ToString();

            if (dbName == null)
            {
                throw new InvalidOperationException("Missing database name");
            }

            return dbName;
        }
    }
}
