/*
 * Copyright (C) 2011 by ForNeVeR
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using Hell.FirstCircle;

namespace Hell.Gateman
{
    /// <summary>
    /// Gateman data storage.
    /// </summary>
    class Storage : IDisposable
    {
        /// <summary>
        /// Connection to database.
        /// </summary>
        private DbConnection _connection;
        
        /// <summary>
        /// Opens a database (creates if not exists).
        /// </summary>
        /// <param name="fileName">Name of file to open.</param>
        public Storage(string fileName)
        {
            _connection = new SQLiteConnection(string.Format("Data Source={0}", fileName));
            _connection.Open();
            CreateTableIfNotExists();
        }

        /// <summary>
        /// Disposes this data storage object.
        /// </summary>
        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        /// <summary>
        /// Adds a record to database corresponding to contact status change.
        /// </summary>
        /// <param name="contact">A contact whom status is changed.</param>
        /// <param name="status">New contact status itself.</param>
        /// <param name="dateTime">A date and time of event.</param>
        public void StoreStatus(Contact contact, Contact.Status status, DateTime dateTime)
        {
            using (var insertCommand = _connection.CreateCommand())
            {
                insertCommand.CommandText = @"
INSERT INTO Gateman (Protocol, Uid, DateTime, Status)
VALUES (:Protocol, :Uid, :DateTime, :Status);
";
                insertCommand.AddParameter("Protocol", contact.Protocol);
                insertCommand.AddParameter("Uid", contact.Uid);
                insertCommand.AddParameter("DateTime", dateTime);
                insertCommand.AddParameter("Status", status);
                insertCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Creates table for usage by storage.
        /// </summary>
        private void CreateTableIfNotExists()
        {
            using (var createCommand = _connection.CreateCommand())
            {
                createCommand.CommandText = @"
CREATE TABLE IF NOT EXISTS Gateman
(
    Protocol TEXT,
    Uid TEXT,
    DateTime TEXT,
    Status INT
);
";
                createCommand.ExecuteNonQuery();
            }
        }
    }
}
