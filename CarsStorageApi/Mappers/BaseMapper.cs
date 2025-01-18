using Riok.Mapperly.Abstractions;

namespace CarsStorageApi.Mappers
{
	[Mapper]
	public partial class BaseMapper<T_in, T_out>
		where T_in : class
		where T_out : class
	{
//		public partial T_out TinToTout<T_in, T_out> (T_in t_in);
//		public partial T_in ToutToTin<T_out, T_in> (T_out t_out);
	}
}
