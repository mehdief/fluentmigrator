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

using FluentMigrator.Runner.Generators;
using FluentMigrator.Runner.Generators.MySql;

using NUnit.Framework;

using Shouldly;

namespace FluentMigrator.Tests.Unit.Generators.MariaDB
{
    [TestFixture]
    [Category("Generator")]
    [Category("Quoter")]
    [Category("MariaDB")]
    public class MariaDBQuoterTests
    {
        private IQuoter _quoter = default(MariaDBQuoter);

        [SetUp]
        public void SetUp()
        {
            _quoter = new MariaDBQuoter();
        }

        [Test]
        public void NewGuidIsFormattedAsUuidFunction()
        {
            _quoter.QuoteValue(SystemMethods.NewGuid).ShouldBe("UUID()");
        }

        [Test]
        public void NewSequentialIdIsFormattedAsUuidFunction()
        {
            _quoter.QuoteValue(SystemMethods.NewSequentialId).ShouldBe("UUID()");
        }

        [Test]
        public void CurrentDateTimeIsFormattedAsCurrentTimestampFunction()
        {
            _quoter.QuoteValue(SystemMethods.CurrentDateTime).ShouldBe("CURRENT_TIMESTAMP(6)");
        }

        [Test]
        public void CurrentUTCDateTimeIsFormattedAsUtcTimestampFunction()
        {
            _quoter.QuoteValue(SystemMethods.CurrentUTCDateTime).ShouldBe("UTC_TIMESTAMP(6)");
        }

        [Test]
        public void CurrentUserIsFormattedAsCurrentUserFunction()
        {
            _quoter.QuoteValue(SystemMethods.CurrentUser).ShouldBe("CURRENT_USER()");
        }

        [Test]
        public void TimeSpanIsFormattedQuotes()
        {
            _quoter.QuoteValue(new TimeSpan(4, 4, 14, 55, 99)).ShouldBe("'100:14:55.099'");
        }

        [Test]
        public void DateTimeIsFormattedQuotes()
        {
            _quoter.QuoteValue(DateTime.Parse("2015-10-5 17:15:10.9999999"))
                .ShouldBe("'2015-10-05T17:15:10.999999'");
        }

        [Test]
        public void DateTimeOffsetIsFormattedQuotes()
        {
            _quoter.QuoteValue(DateTimeOffset.Parse("2015-10-5 17:15:10.9999999+0100"))
                .ShouldBe("'2015-10-05T17:15:10.999999'");
        }

#if NET6_0_OR_GREATER
        [Test]
        public void TimeOnlyIsFormattedQuotes()
        {
            _quoter.QuoteValue(TimeOnly.Parse("17:15:10.9999999")).ShouldBe("'17:15:10.999999'");
        }
#endif
    }
}
