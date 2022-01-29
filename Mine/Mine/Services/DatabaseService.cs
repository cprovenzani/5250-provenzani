﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

using Mine.Models;


namespace Mine.Services
{
    public class DatabaseService : IDataStore<ItemModel>
    {
        static readonly Lazy<SQLiteAsyncConnection> LazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => LazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        /// <summary>
        /// Insert item into Database
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if it worked, else false</returns>
        public async Task<bool> CreateAsync(ItemModel item)
        {
            //return false if item is null
            if (item == null)
            {
                return false;
            }
            //write to table
            var result = await Database.InsertAsync(item);
            //return false if returned ID is 0
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public Task<bool> UpdateAsync(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemModel> ReadAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get list of items from database
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns>list of items</returns>
        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            //list of items returned from database
            var result = await Database.Table<ItemModel>().ToListAsync();
            return result;
        }
    }
}