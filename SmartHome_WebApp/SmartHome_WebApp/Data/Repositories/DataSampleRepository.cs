using Microsoft.EntityFrameworkCore;
using SmartHome_WebApp.Models;
using SmartHomeWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SmartHome_WebApp.Data.Repositories
{
    public class DataSampleRepository : IRepository<DataSample>
    {

        #region Interface Members

        public async Task<bool> Add(DataSample newElement)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    await context.DataSamples.AddAsync(newElement);

                    //Checking if the saving method succeeds
                    if (await context.SaveChangesAsync() != 1)
                    {
                        throw new ApplicationException("Saving data was uncusseful!");
                    }
                }
                return true;
            }

            catch(ApplicationException exc)
            {
                return false;
            }
            catch(DbUpdateException exc)
            {
                return false;
            }
        }

        public async Task<List<DataSample>> Find(Expression<Func<DataSample, bool>> queryLambda)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.DataSamples.Where(queryLambda).ToListAsync();

                    //Returnning the result list.
                    //If no element found, we return null.
                    return result;
                }
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public async Task<bool> Modify(DataSample toModify)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.DataSamples.FirstAsync(f => f.SenderId == toModify.SenderId && f.TimeStamp == toModify.TimeStamp);
                    
                    //Check if the element we need is i  the database
                    if (result == null)
                    {
                        throw new NullReferenceException("No member like the given parameter was present in the DB");
                    }

                    result = toModify;

                    //Checking if the saving method succeeds
                    if (await context.SaveChangesAsync() != 1)
                    {
                        throw new ApplicationException("Saving data was uncusseful!");
                    }
                }
                return true;
            }
            catch(Exception exc)
            {
                return false;
            }
        }

        public async Task<bool> Remove(DataSample toDelete)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.DataSamples.FirstAsync(wer => toDelete.TimeStamp == wer.TimeStamp && toDelete.SenderId == wer.SenderId);

                    //Check if the element we need is i  the database
                    if (result == null)
                    {
                        throw new NullReferenceException("No member like the given parameter was present in the DB");
                    }
                    context.DataSamples.Remove(result);

                    //Checking if the saving method succeeds
                    if (await context.SaveChangesAsync() != 1)
                    {
                        throw new ApplicationException("Saving data was uncusseful!");
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public async Task<bool> Remove(Guid toRemoveId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.DataSamples.FirstAsync(wer => wer.SamplingId == toRemoveId);

                    //Check if the element we need is i  the database
                    if (result == null)
                    {
                        throw new NullReferenceException("No member like the given parameter was present in the DB");
                    }

                    context.DataSamples.Remove(result);

                    //Checking if the saving method succeeds
                    if (await context.SaveChangesAsync() != 1)
                    {
                        throw new ApplicationException("Saving data was uncusseful!");
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        #endregion
    }
}
