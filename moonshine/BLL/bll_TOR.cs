using moonshine.DAL;
using moonshine.Util;

namespace moonshine.BLL
{
    public class bll_TOR
    {
        public static string getBData(string sdate, string edate)
        {
            string sqlStr = @"select  b.basic_id,b.description,b.spec,b.price,sum(i.in_qty) as in_qty,sum(o.out_qty) as out_qty,
                                (select if(isnull(sum(in_qty)),0,sum(in_qty)) from in_data where in_id=b.basic_id and in_date <='{0}')
                                -(select if(isnull(sum(out_qty)),0,sum(out_qty)) from out_data where out_id=b.basic_id and out_date <='{0}') as 'IN-OUT1',
                                (select if(isnull(sum(in_qty)),0,sum(in_qty)) from in_data where in_id=b.basic_id and in_date <='{1}')
                                -(select if(isnull(sum(out_qty)),0,sum(out_qty)) from out_data where out_id=b.basic_id and out_date <='{1}') as 'IN-OUT2'
                                from basic_data b
                                left join in_data  i on  b.basic_id=i.in_id  and i.in_date between '{0}' and '{1}'
                                left join out_data o on b.basic_id = o.out_id and o.out_date between '{0}' and '{1}'
                                group by b.basic_id; ";

            sqlStr = string.Format(sqlStr, sdate, edate);
            return JsonHelper.ToJson(MysqlHelper.ExecuteReader(sqlStr));
        }
    }
}