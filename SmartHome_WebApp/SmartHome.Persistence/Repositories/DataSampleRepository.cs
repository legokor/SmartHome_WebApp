using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SmartHome.Model;

namespace SmartHome.Persistence.Repositories
{
    public class DataSampleRepository : IRepository<DataSample>
    {

        #region Interface Members

        public async Task<bool> AddAsync(DataSample newElement)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var masterUnit = await context.MasterUnits.SingleOrDefaultAsync(wer => wer.Id == newElement.MasterUnitId);
                    if (masterUnit == null)
                    {
                        return false;
                    }
                    newElement.MasterUnit = masterUnit;

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

        public async Task<List<DataSample>> FindListAsync(Expression<Func<DataSample, bool>> queryLambda)
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

        public async Task<DataSample> FindAsync(Expression<Func<DataSample, bool>> queryLambda)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.DataSamples.SingleOrDefaultAsync(queryLambda);

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

        public async Task<bool> ModifyAsync(DataSample toModify)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.DataSamples.FirstAsync(f => f.MasterUnitId == toModify.MasterUnitId && f.TimeStamp == toModify.TimeStamp);
                    
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

        public async Task<bool> RemoveAsync(DataSample toDelete)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.DataSamples.FirstAsync(wer => toDelete.TimeStamp == wer.TimeStamp && toDelete.MasterUnitId == wer.MasterUnitId);

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

        public async Task<bool> RemoveAsync(Guid toRemoveId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.DataSamples.FirstAsync(wer => wer.SampleId == toRemoveId);

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
