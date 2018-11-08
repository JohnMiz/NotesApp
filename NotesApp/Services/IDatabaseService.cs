using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public interface IDatabaseService<T>
		  where T : class
    {
			   
		  bool Insert(T item);


		  bool Update(T item);


		  bool Delete(T item);
		  
	 }
}
