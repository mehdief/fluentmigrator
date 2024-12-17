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
        private const string TABLE_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}'";
        private const string COLUMN_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}' AND COLUMN_NAME = '{1}'";
        private const string CONSTRAINT_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}' AND CONSTRAINT_NAME = '{1}'";
        private const string INDEX_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS WHERE TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}' AND INDEX_NAME = '{1}'";
        // private const string SEQUENCES_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'SEQUENCE' AND TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}'"; ver < 11.5
        private const string SEQUENCES_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.SEQUENCES WHERE SEQUENCE_SCHEMA = SCHEMA() AND SEQUENCE_NAME = '{0}'"; // ver >=11.5
        private const string DEFAULT_VALUE_EXISTS = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = SCHEMA() AND TABLE_NAME = '{0}' AND COLUMN_NAME = '{1}' AND COLUMN_DEFAULT LIKE '{2}'";

        private readonly MariaDBQuoter _quoter = new MariaDBQuoter();

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

        public override bool Exists(string template, params object[] args)
        {
            EnsureConnectionIsOpen();

            var commandText = "SELECT EXISTS (" + string.Format(template, args) + ")";
            using (var command = CreateCommand(commandText))
            {
                var result = command.ExecuteScalar();
                return !Convert.IsDBNull(result) && Convert.ToInt32(result) == 1;
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
    }
}
