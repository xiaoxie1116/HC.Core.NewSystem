using SqlSugar;
using System;

namespace HC.Core.DB.Entitys
{
    /// <summary>
    /// 订单列表
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public Int32 OrderID { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public Int32? ProductId { get; set; }

        /// <summary>
        /// 团ID
        /// </summary>
        public Int32? TourID { get; set; }

        /// <summary>
        /// 销售ID
        /// </summary>
        public Int32? SalesID { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Int32? CustomerID { get; set; }

        /// <summary>
        /// 出行日期
        /// </summary>
        public DateTime? TravelDate { get; set; }

        /// <summary>
        /// 成人数
        /// </summary>
        public Int32? AdultNo { get; set; }

        /// <summary>
        /// 儿童数
        /// </summary>
        public Int32? ChildNo { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public Int32? OrderStatus { get; set; }

        /// <summary>
        /// 订单操作状态（二进制）
        /// </summary>
        public Int32? OperateStatus { get; set; }

        /// <summary>
        /// 订单支付状态
        /// </summary>
        public Int32? PayStatus { get; set; }

        /// <summary>
        /// 订单总价
        /// </summary>
        public Decimal? TotalPrice { get; set; }

        /// <summary>
        /// 应收价格
        /// </summary>
        public Decimal? ReceivablePrice { get; set; }

        /// <summary>
        /// 销售备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Int32? CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public Int32? ModifyUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 老人数
        /// </summary>
        public Int32? OldNo { get; set; }

        /// <summary>
        /// 婴儿数
        /// </summary>
        public Int32? BabyNo { get; set; }

        /// <summary>
        /// 销售类型：1同行，2直客3.领队导游
        /// </summary>
        public String SalesType { get; set; }

        /// <summary>
        /// 订单来源1正常单2组团3拼团订单
        /// </summary>
        public Int16? OrderSource { get; set; }

        /// <summary>
        /// 订单类型1正常单（销售单）2自动领队单3自动导游单，4客户下领队单5客户下导游单6.客户下单
        /// </summary>
        public Int16? OrderType { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public Int32? Confirm { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 补位人
        /// </summary>
        public Int32? Complement { get; set; }

        /// <summary>
        /// 补位时间
        /// </summary>
        public DateTime? ComplementTime { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Int32? FK_Department { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public Int32? FK_Company { get; set; }

        /// <summary>
        /// 清位截止时间
        /// </summary>
        public DateTime? CleanBlockingTime { get; set; }

        /// <summary>
        /// 清位规则ID
        /// </summary>
        public Int32? FK_CleanOrderRule { get; set; }

        /// <summary>
        /// 清位使用时间
        /// </summary>
        public DateTime? CleanUsedTime { get; set; }

        /// <summary>
        ///  0未知1清位2延迟清位
        /// </summary>
        public Int16? CleanOrderRuleType { get; set; }

        /// <summary>
        /// 成人价
        /// </summary>
        public Decimal? AdultPrice { get; set; }

        /// <summary>
        /// 儿童价
        /// </summary>
        public Decimal? ChildPrice { get; set; }

        /// <summary>
        /// 婴儿价
        /// </summary>
        public Decimal? BabyPrice { get; set; }

        /// <summary>
        /// 老人价
        /// </summary>
        public Decimal? OldPrice { get; set; }

        /// <summary>
        /// 是否有效（T:F）
        /// </summary>
        public String IsValid { get; set; }

        /// <summary>
        /// 单房差
        /// </summary>
        public Decimal? SingleRoomPrice { get; set; }

        /// <summary>
        /// 退团原因
        /// </summary>
        public String ReturnReason { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public Decimal? ReceivedAmount { get; set; }

        /// <summary>
        /// 返佣类型 0无返佣，1待返佣，2已返佣
        /// </summary>
        public Int32? RebateStatus { get; set; }

        /// <summary>
        /// 团价格ID
        /// </summary>
        public Int32? FK_TourPrice { get; set; }

        /// <summary>
        /// 满团清位状态 0.默认 1.待满团清位，2.已满团清位
        /// </summary>
        public Int32? TourZeroStatus { get; set; }

        /// <summary>
        /// 前返金额总和
        /// </summary>
        public Decimal? Commision { get; set; }

        /// <summary>
        /// 订单取消原因
        /// </summary>
        public Int32? CancelReason { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public Int32? Applicant { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? ApplicationDate { get; set; }

        /// <summary>
        /// 已开发票总金额
        /// </summary>
        public Decimal? TotalInvoiceAmount { get; set; }

        /// <summary>
        /// 应收价格变动确认状态，用于财务人员确认金额调整0未确认，1已确认
        /// </summary>
        public Int32? ReceivablePriceStatus { get; set; }

        /// <summary>
        /// 应收价格确认时间
        /// </summary>
        public DateTime? ReceivablePriceConfirmTime { get; set; }

        /// <summary>
        /// 应收价格确认人
        /// </summary>
        public Int32? ReceivablePriceConfirmUser { get; set; }

        /// <summary>
        /// 自动领队导游订单需要增加导游id
        /// </summary>
        public Int32? FK_Guide { get; set; }

        /// <summary>
        /// 记账客户
        /// </summary>
        public Int32? AccountCustomerID { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public Int32? FK_Currency { get; set; }

        /// <summary>
        /// 合同状态:1-合同未发送 2-合同已发送未确认 3-合同已发送已确认
        /// </summary>
        public Int32? ContractStatus { get; set; }

        /// <summary>
        /// 报名日期
        /// </summary>
        public DateTime? EnrollTime { get; set; }

        /// <summary>
        /// 合同ID
        /// </summary>
        public Int32? FK_ContractID { get; set; }

        /// <summary>
        /// 实际应收价格
        /// </summary>
        public Decimal? RealReceivablePrice { get; set; }

        /// <summary>
        /// 滞纳金
        /// </summary>
        public Decimal? Penalty { get; set; }

        /// <summary>
        /// 后返金额
        /// </summary>
        public Decimal? Rebate { get; set; }

        /// <summary>
        /// 欠款
        /// </summary>
        public Decimal? Arrearages { get; set; }

        /// <summary>
        /// 不需要返现的金额
        /// </summary>
        public Decimal? NoCommision { get; set; }

        /// <summary>
        /// 价格Id
        /// </summary>
        public Int32? PriceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? ManualRealReceivableChange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? SettlementType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? SettlementDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? PenaltyStartDate { get; set; }

        /// <summary>
        /// 认领id
        /// </summary>
        public Int32? SupporterId { get; set; }

        /// <summary>
        /// 认领部门
        /// </summary>
        public Int32? SupporterDepartment { get; set; }

        /// <summary>
        /// 认领公司
        /// </summary>
        public Int32? SupporterCompany { get; set; }

        /// <summary>
        /// 认领时间
        /// </summary>
        public DateTime? ClaimTime { get; set; }

        /// <summary>
        /// 认领状态（1：未认领 2：已认领）
        /// </summary>
        public Int32? ClaimStatus { get; set; }
    }
}
