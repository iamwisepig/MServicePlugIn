using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Orm.DataEntity;
using Kingdee.BOS.Core.Validation;
using Kingdee.BOS.Core;
using Kingdee.BOS.App.Data;

namespace MServicePlubIn.ServicePlugIn
{
    public class MyOperationPlugin : AbstractOperationServicePlugIn
    {
        public override void OnPreparePropertys(PreparePropertysEventArgs e)
        {
            //e.FieldKeys.Add("");将需要应用的字段Key加入
        }

        /// <summary>
        /// 添加校验器
        /// </summary>
        /// <param name="e"></param>
        public override void OnAddValidators(AddValidatorsEventArgs e)
        {
            //var operValidator = new OperValidator();
            //operValidator.AlwaysValidate = true;
            //operValidator.EntityKey = "FBillHead";
            //e.Validators.Add(operValidator);
        }

        /// <summary>
        /// 操作开始前功能处理
        /// </summary>
        /// <param name="e"></param>
        public override void BeginOperationTransaction(BeginOperationTransactionArgs e)
        {                        
            foreach (DynamicObject o in e.DataEntitys)
            {
                string strSql = string.Format("SELECT A.F_PCQE_TSSDID,A.F_PCQE_SDBTG,A.F_PCQE_SDTG,A.F_PCQE_SDXZG,A.F_PCQE_WXSD,B.F_PCQE_ZGYJ FROM PCQE_T_TSSSD A LEFT JOIN PCQE_T_TSSSD_L B ON A.FID = B.FID AND B.FLocaleID=2052 AND A.FID = '{0}'",  Convert.ToInt64(o["ID"].ToString()));
                DynamicObjectCollection obj = DBUtils.ExecuteDynamicObject(this.Context,strSql);
                DynamicObject tempobj = obj[0];
                strSql = "Update PCQE_t_TSSDJSJ SET F_PCQE_WSD =0, F_PCQE_SDBTG='" + tempobj["F_PCQE_SDBTG"].ToString() + "'," +
                                    "F_PCQE_SDTG='" + tempobj["F_PCQE_SDTG"].ToString() + "'," +
                                    "F_PCQE_SDXZG='" + tempobj["F_PCQE_SDXZG"].ToString() + "'," +
                                    "F_PCQE_WXSD='" + tempobj["F_PCQE_WXSD"].ToString() + "'  WHERE FID=" + Convert.ToInt32(tempobj["F_PCQE_TSSDID"]) +
                                    "  Update PCQE_t_TSSDJSJ_L SET F_PCQE_ZGYJ='" + tempobj["F_PCQE_ZGYJ"].ToString() + "' WHERE FLocaleID=2052 AND FID=" + Convert.ToInt32(tempobj["F_PCQE_TSSDID"]);
                DBUtils.Execute(this.Context, strSql);
            }
        }

        /// <summary>
        /// 操作结束后功能处理
        /// </summary>
        /// <param name="e"></param>
        public override void EndOperationTransaction(EndOperationTransactionArgs e)
        {
            foreach (DynamicObject o in e.DataEntitys)
            {
            }
        }


        /// <summary>
        /// 当前操作的校验器
        /// </summary>
        private class OperValidator : AbstractValidator
        {
            public override void Validate(Kingdee.BOS.Core.ExtendedDataEntity[] dataEntities, ValidateContext validateContext, Kingdee.BOS.Context ctx)
            {
                //foreach (var dataEntity in dataEntities)
                //{
                //判断到数据有错误
                //    if()
                //    {
                //        ValidationErrorInfo ValidationErrorInfo = new ValidationErrorInfo(
                //            string.Empty,
                //            dataEntity["Id"].ToString(),
                //            dataEntity.DataEntityIndex,
                //            dataEntity.RowIndex,
                //            dataEntity["Id"].ToString(),
                //            "errMessage",
                //             string.Empty);
                //        validateContext.AddError(null, ValidationErrorInfo);
                //        continue;
                //    }

                //}
            }
        }
    }
}
