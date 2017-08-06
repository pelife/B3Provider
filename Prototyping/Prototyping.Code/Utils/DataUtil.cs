using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Prototyping.Code.Utils
{

    /// -----------------------------------------------------------------------------
    /// Project	 : .Framework.Common
    /// Class	 : Framework..Framework.Common.DataUtil
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    ///     Classe de funcionalidades relacionadas ao manuseamento de classes ADO.Net
    ///     (System.Data - DataSet, DataTable, DataColumn, DataView, DataRow, ...)
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// 	[ofilho]	02/03/2006	Created
    /// </history>
    /// -----------------------------------------------------------------------------
    public class DataUtil
    {

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Percorre todos as linhas e colunas do datatable passado verificando se
        ///     realmente existe diferenca entre o valor original e o atual.
        /// </summary>
        /// <param name="rDtt_Temp"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static bool CompareColumValues(ref System.Data.DataTable rDtt_Temp)
        {
            bool functionReturnValue = false;
            System.Data.DataRow lDtr_Temp = null;
            short lSht_Col = 0;

            // padrao original = atual
            functionReturnValue = true;

            // se tem

            if ((rDtt_Temp != null))
            {
                // percorre todas as linhas

                foreach (DataRow lDtr_Temp_loopVariable in rDtt_Temp.Rows)
                {
                    lDtr_Temp = lDtr_Temp_loopVariable;
                    // tem corrente e proposto?

                    if ((lDtr_Temp.HasVersion(DataRowVersion.Current) == true) & (lDtr_Temp.HasVersion(DataRowVersion.Proposed) == true))
                    {
                        // percorre todas as colunas

                        for (lSht_Col = 0; lSht_Col <= rDtt_Temp.Columns.Count - 1; lSht_Col++)
                        {
                            // se um for nulo e o outro nao
                            if (DBNull.Value.Equals(lDtr_Temp[lSht_Col, DataRowVersion.Current]) ^ DBNull.Value.Equals(lDtr_Temp[lSht_Col, DataRowVersion.Proposed]))
                            {
                                // nao sao iguais
                                return false;
                            }
                            else
                            {
                                // nao pode ser nulos
                                if (!DBNull.Value.Equals(lDtr_Temp[lSht_Col, DataRowVersion.Current]) & !DBNull.Value.Equals(lDtr_Temp[lSht_Col, DataRowVersion.Proposed]))
                                {
                                    // compara original e atual
                                    if ((lDtr_Temp[lSht_Col, DataRowVersion.Current] != lDtr_Temp[lSht_Col, DataRowVersion.Proposed]))
                                    {
                                        // nao sao iguais
                                        return false;
                                    }

                                }

                            }

                        }

                    }

                }

            }
            return functionReturnValue;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Adicionar a linha ao datatable informado.
        /// </summary>
        /// <param name="vDrv_Source"></param>
        /// <param name="rDtt_Destination"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static System.Data.DataRow AddRowView(System.Data.DataRowView vDrv_Source, ref System.Data.DataTable rDtt_Destination)
        {
            System.Data.DataRow lDtr_Destination = null;
            System.Data.DataColumn lDtc_Temp = null;
            bool lBln_Ok = false;

            // cria uma estrutura para nova linha no destino
            lDtr_Destination = rDtt_Destination.NewRow();

            // percorre as colunas da origem

            foreach (DataColumn lDtc_Temp_loopVariable in vDrv_Source.DataView.Table.Columns)
            {
                lDtc_Temp = lDtc_Temp_loopVariable;
                // tem no destino?

                if ((rDtt_Destination.Columns.Contains(lDtc_Temp.ColumnName) == true))
                {
                    // marca flag para indicar que pelo menos uma coluna foi copiada
                    lBln_Ok = true;

                    // copia
                    lDtr_Destination[lDtc_Temp.ColumnName] = vDrv_Source[lDtc_Temp.ColumnName];

                }

            }

            // nova linha valida?

            if ((lBln_Ok == true))
            {
                // passa para o destino
                rDtt_Destination.Rows.Add(lDtr_Destination);

                // retorna a linha incluida
                return lDtr_Destination;
            }
            else
            {
                return null;
            }

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Adicionar a linha ao datatable informado.
        /// </summary>
        /// <param name="vDtr_Source"></param>
        /// <param name="rDtt_Destination"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static System.Data.DataRow AddRow(System.Data.DataRow vDtr_Source, ref System.Data.DataTable rDtt_Destination)
        {
            System.Data.DataRow lDtr_Destination = null;
            System.Data.DataColumn lDtc_Temp = null;
            bool lBln_Ok = false;

            // cria uma estrutura para nova linha no destino
            lDtr_Destination = rDtt_Destination.NewRow();

            // percorre as colunas da origem

            foreach (DataColumn lDtc_Temp_loopVariable in vDtr_Source.Table.Columns)
            {
                lDtc_Temp = lDtc_Temp_loopVariable;
                // tem no destino?

                if ((rDtt_Destination.Columns.Contains(lDtc_Temp.ColumnName) == true))
                {
                    // marca flag para indicar que pelo menos uma coluna foi copiada
                    lBln_Ok = true;

                    // copia
                    lDtr_Destination[lDtc_Temp.ColumnName] = vDtr_Source[lDtc_Temp.ColumnName];

                }

            }

            // nova linha valida?

            if ((lBln_Ok == true))
            {
                // passa para o destino
                rDtt_Destination.Rows.Add(lDtr_Destination);

                // retorna a linha incluida
                return lDtr_Destination;
            }
            else
            {
                return null;
            }

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Adicionar a linha ao datatable informado.
        /// </summary>
        /// <param name="vDtr_Source"></param>
        /// <param name="rDtt_Destination"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static System.Data.DataRow[] AddRows(System.Data.DataRow[] vDtr_Source, ref System.Data.DataTable rDtt_Destination)
        {
            System.Data.DataRow lDtr_Destination = null;
            System.Data.DataColumn lDtc_Temp = null;
            bool lBln_Ok = false;
            DataRow lDtr_Source = null;
            ArrayList lObj_NewRows = new ArrayList();


            foreach (DataRow lDtr_Source_loopVariable in vDtr_Source)
            {
                lDtr_Source = lDtr_Source_loopVariable;
                // cria uma estrutura para nova linha no destino
                lDtr_Destination = rDtt_Destination.NewRow();

                // percorre as colunas da origem

                foreach (DataColumn lDtc_Temp_loopVariable in lDtr_Source.Table.Columns)
                {
                    lDtc_Temp = lDtc_Temp_loopVariable;
                    // tem no destino?

                    if ((rDtt_Destination.Columns.Contains(lDtc_Temp.ColumnName) == true))
                    {
                        // marca flag para indicar que pelo menos uma coluna foi copiada
                        lBln_Ok = true;

                        // copia
                        lDtr_Destination[lDtc_Temp.ColumnName] = lDtr_Source[lDtc_Temp.ColumnName];

                    }

                }

                // nova linha valida?

                if ((lBln_Ok == true))
                {
                    // passa para o destino
                    rDtt_Destination.Rows.Add(lDtr_Destination);

                    lObj_NewRows.Add(lDtr_Destination);

                }
            }

            // retorna a linha incluida
            return (DataRow[]) lObj_NewRows.ToArray(typeof(DataRow));

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Adicionar a view ao datatable informado.
        /// </summary>
        /// <param name="rDtv_Source"></param>
        /// <param name="rDtt_Destination"></param>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void AddView(ref System.Data.DataView rDtv_Source, ref System.Data.DataTable rDtt_Destination)
        {
            System.Data.DataRowView lDrv_Sorce = null;

            // inicia insercao
            rDtt_Destination.BeginLoadData();

            // percorre e importa
            foreach (DataRowView lDrv_Sorce_loopVariable in rDtv_Source)
            {
                lDrv_Sorce = lDrv_Sorce_loopVariable;
                // copia a linha atual
                AddRowView(lDrv_Sorce, ref rDtt_Destination);
            }

            // fim
            rDtt_Destination.EndLoadData();

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Obtem uma view das linhas que sofreram alteração.
        /// </summary>
        /// <param name="rDtt_Table"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static System.Data.DataView GetUpdatedView(ref System.Data.DataTable rDtt_Table)
        {
            System.Data.DataTable lDtt_Temp = null;
            System.Data.DataView lDtv_Temp = null;

            // padrao
            lDtv_Temp = null;

            // valido?

            if ((rDtt_Table == null) == false)
            {
                // obtem as linhas alteradas
                lDtt_Temp = rDtt_Table.GetChanges(DataRowState.Modified);

                // tem?
                if ((lDtt_Temp == null) == false)
                {
                    // cria a view
                    lDtv_Temp = rDtt_Table.GetChanges().DefaultView;
                    // seta para pegar somente os alterados
                    lDtv_Temp.RowStateFilter = (DataViewRowState) DataRowState.Modified;
                }
                else
                {
                    // se ainda ta vazio retorna so o esqueleto
                    lDtv_Temp = rDtt_Table.Clone().DefaultView;
                }

            }

            // retorna
            return lDtv_Temp;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Obtem uma view das linhas que sofreram exclusão.
        /// </summary>
        /// <param name="rDtt_Table"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static System.Data.DataView GetDeletedView(ref System.Data.DataTable rDtt_Table)
        {
            System.Data.DataTable lDtt_Temp = null;
            System.Data.DataView lDtv_Temp = null;

            // padrao
            lDtv_Temp = null;

            // valido?

            if ((rDtt_Table == null) == false)
            {
                // obtem as linhas alteradas
                lDtt_Temp = rDtt_Table.GetChanges(DataRowState.Deleted);

                // tem?
                if ((lDtt_Temp == null) == false)
                {
                    // cria a view
                    lDtv_Temp = rDtt_Table.GetChanges().DefaultView;
                    // seta para pegar somente os alterados
                    lDtv_Temp.RowStateFilter = (DataViewRowState) DataRowState.Deleted;
                }
                else
                {
                    // se ainda ta vazio retorna so o esqueleto
                    lDtv_Temp = rDtt_Table.Clone().DefaultView;
                }

            }

            // retorna
            return lDtv_Temp;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Obtem uma view das linhas que sofreram inclusão.
        /// </summary>
        /// <param name="rDtt_Table"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static System.Data.DataView GetInsertedView(ref System.Data.DataTable rDtt_Table)
        {
            System.Data.DataTable lDtt_Temp = null;
            System.Data.DataView lDtv_Temp = null;
            System.Data.DataRowView lDrv_Temp = null;

            // padrao
            lDtv_Temp = null;

            // valido?

            if ((rDtt_Table == null) == false)
            {
                // se tiver mais registro na view que no datatable significa
                // que existe uma inclusao no dataview que ainda nao
                // foi sincronizada no datatable, isso acontece
                // quando é necessário buscar os dados ocorridos no 
                // datagrid no momento de disparo do evento LineInserted

                if ((rDtt_Table.Rows.Count < rDtt_Table.DefaultView.Count))
                {
                    // copia estrutura
                    lDtt_Temp = rDtt_Table.Clone();

                    // percorre as linhas da estrutura original

                    foreach (DataRowView lDrv_Temp_loopVariable in rDtt_Table.DefaultView)
                    {
                        lDrv_Temp = lDrv_Temp_loopVariable;
                        // de acordo com o estado da linha
                        switch (lDrv_Temp.Row.RowState)
                        {

                            case DataRowState.Added:
                                // linhas incluidas sao consideradas
                                lDtt_Temp.ImportRow(lDrv_Temp.Row);
                                break;
                        }

                    }


                }
                else
                {
                    // obtem as linhas alteradas
                    lDtt_Temp = rDtt_Table.GetChanges(DataRowState.Added);

                }

                // tem?
                if (((lDtt_Temp == null) == false) && ((lDtt_Temp.GetChanges() == null) == false))
                {
                    // cria a view
                    lDtv_Temp = lDtt_Temp.GetChanges().DefaultView;
                    // seta para pegar somente os alterados
                    lDtv_Temp.RowStateFilter = (DataViewRowState) DataRowState.Added;
                }
                else
                {
                    // retorna so o esqueleto
                    lDtv_Temp = rDtt_Table.Clone().DefaultView;
                }

            }

            // retorna
            return lDtv_Temp;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Obtem uma view das linhas que sofreram exclusão, inclusão ou alteração.
        /// </summary>
        /// <param name="rDtt_Table"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static System.Data.DataView GetModifiedView(ref System.Data.DataTable rDtt_Table)
        {
            System.Data.DataTable lDtt_Temp = null;
            System.Data.DataView lDtv_Temp = null;
            System.Data.DataRowView lDrv_Temp = null;

            // padrao
            lDtv_Temp = null;

            // valido?

            if ((rDtt_Table == null) == false)
            {
                // se tiver mais registro na view que no datatable significa
                // que existe uma inclusao no dataview que ainda nao
                // foi sincronizada no datatable, isso acontece
                // quando é necessário buscar os dados ocorridos no 
                // datagrid no momento de disparo do evento LineInserted

                if ((rDtt_Table.Rows.Count < rDtt_Table.DefaultView.Count))
                {
                    // copia estrutura
                    lDtt_Temp = rDtt_Table.Clone();

                    // percorre as linhas da estrutura original

                    foreach (DataRowView lDrv_Temp_loopVariable in rDtt_Table.DefaultView)
                    {
                        lDrv_Temp = lDrv_Temp_loopVariable;
                        // de acordo com o estado da linha
                        switch (lDrv_Temp.Row.RowState)
                        {

                            case DataRowState.Added:
                            case DataRowState.Deleted:
                            case DataRowState.Modified:
                                // linhas incluidas, alteradas ou excluidas sao consideradas
                                lDtt_Temp.ImportRow(lDrv_Temp.Row);
                                break;
                        }

                    }


                }
                else
                {
                    // obtem as linhas alteradas
                    lDtt_Temp = rDtt_Table.GetChanges(DataRowState.Added + (int) DataRowState.Deleted + (int) DataRowState.Modified);

                }

                // tem?
                if (((lDtt_Temp == null) == false) && ((lDtt_Temp.GetChanges() == null) == false))
                {
                    // cria a view
                    lDtv_Temp = lDtt_Temp.GetChanges().DefaultView;
                    // seta para pegar somente os alterados
                    lDtv_Temp.RowStateFilter = (DataViewRowState) (DataRowState.Added + (int)DataRowState.Deleted + (int)DataRowState.Modified);
                }
                else
                {
                    // retorna so o esqueleto
                    lDtv_Temp = rDtt_Table.Clone().DefaultView;
                }

            }

            // retorna
            return lDtv_Temp;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Procura a linha do datatable que possui o valor na coluna.
        /// </summary>
        /// <param name="vDtt_Table"></param>
        /// <param name="vStr_ColumnName"></param>
        /// <param name="vObj_Value"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DataRow FindRow(DataTable vDtt_Table, string vStr_ColumnName, object vObj_Value)
        {
            string lStr_OldSort = null;
            int lInt_RowIndex = 0;
            DataRow lDtr_Return = null;

            // guarda a ordenacao
            lStr_OldSort = vDtt_Table.DefaultView.Sort;

            // ordena pela coluna desejada
            vDtt_Table.DefaultView.Sort = vStr_ColumnName;
            lInt_RowIndex = vDtt_Table.DefaultView.Find(vObj_Value);

            // achou alguem?
            if ((lInt_RowIndex != -1))
            {
                // retorna a linha achada
                lDtr_Return = vDtt_Table.DefaultView[lInt_RowIndex].Row;
            }

            // volta a ordenacao anterior
            vDtt_Table.DefaultView.Sort = lStr_OldSort;

            // retorna a linha
            return lDtr_Return;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Copia as coluna da linha de origem que estão na linha destino.
        /// </summary>
        /// <param name="vDtr_Source"></param>
        /// <param name="rDtr_Destination"></param>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	02/03/2006	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void CopyRowValues(System.Data.DataRow vDtr_Source, ref System.Data.DataRow rDtr_Destination)
        {
            System.Data.DataColumn lObj_Column = null;

            // percorre as colunas
            foreach (DataColumn lObj_Column_loopVariable in vDtr_Source.Table.Columns)
            {
                lObj_Column = lObj_Column_loopVariable;
                // destino possui essa coluna?
                if ((rDtr_Destination.Table.Columns.Contains(lObj_Column.ColumnName) == true))
                {
                    // copia valores
                    rDtr_Destination[lObj_Column.ColumnName] = vDtr_Source[lObj_Column.ColumnName];
                }
            }

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Ajusta uma string para utilização como filtro para o datatable.
        /// </summary>
        /// <param name="vStr_Value"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	30/11/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static string AdjustString(string vStr_Value)
        {
            // Procura na string uma aspas simples para duplica-la, pois
            // deste modo pode-se assegurar que vai ser inserido
            // o numero certo de aspas simples no BD
            return vStr_Value.Replace("'", "''");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Ajusta uma data para utilização como filtro para o datatable.
        /// </summary>
        /// <param name="vDat_Date"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	30/11/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static string AdjustDate(System.DateTime vDat_Date)
        {
            return vDat_Date.ToString("yyyy/MM/dd");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Ajusta uma data e hora para utilização como filtro para o datatable.
        /// </summary>
        /// <param name="vDat_Date"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	30/11/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static string AdjustDateAndTime(System.DateTime vDat_Date)
        {
            return vDat_Date.ToString("yyyy/MM/dd HH:mm:ss");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Ajustar um numero para utilização como filtro para o datatable.
        /// </summary>
        /// <param name="vDbl_Number"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[ofilho]	30/11/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static string AdjustNumber(double vDbl_Number)
        {
            return vDbl_Number.ToString("#0.00000000");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Altera o valor de uma coluna informada dentre as linhas para um certo valor informado
        /// </summary>
        /// <param name="rDtt_Data"></param>
        /// <param name="vStr_ColumnName"></param>
        /// <param name="vObj_ColumnValue"></param>
        /// <param name="vStr_WhereClause"></param>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[limaot]	26/03/2007	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Update(ref DataTable rDtt_Data, string vStr_ColumnName, object vObj_ColumnValue, string vStr_WhereClause = null)
        {
            DataRow[] lArr_Rows = null;

            if ((rDtt_Data == null))
            {
                throw new ApplicationException("Update recebeu um DataTable inválido!");
            }
            if ((!rDtt_Data.Columns.Contains(vStr_ColumnName)))
            {
                throw new ApplicationException("Não foi encontrada uma coluna de nome \"" + vStr_ColumnName + "\" no DataTable recebido!");
            }

            if ((vStr_WhereClause == null))
            {
                lArr_Rows = rDtt_Data.Select();
            }
            else
            {
                lArr_Rows = rDtt_Data.Select(vStr_WhereClause);
            }

            Update(ref lArr_Rows, vStr_ColumnName, vObj_ColumnValue);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Altera o valor de uma coluna informada dentre as linhas para um certo valor informado
        /// </summary>
        /// <param name="rDtv_Data"></param>
        /// <param name="vStr_ColumnName"></param>
        /// <param name="vObj_ColumnValue"></param>
        /// <param name="vStr_WhereClause"></param>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[limaot]	26/03/2007	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Update(ref DataView rDtv_Data, string vStr_ColumnName, object vObj_ColumnValue, string vStr_WhereClause = null)
        {
            DataRow[] lArr_Rows = null;
            int lInt_Loop = 0;
            ArrayList lCol_Rows = new ArrayList();
            string lStr_FiltroOriginal = string.Empty;

            if ((rDtv_Data == null))
            {
                throw new ApplicationException("Update recebeu um DataView inválido!");
            }
            if ((!rDtv_Data.Table.Columns.Contains(vStr_ColumnName)))
            {
                throw new ApplicationException("Não foi encontrada uma coluna de nome \"" + vStr_ColumnName + "\" no DataView recebido!");
            }

            if (((vStr_WhereClause != null)))
            {
                lStr_FiltroOriginal = rDtv_Data.RowFilter;
                if ((!string.IsNullOrEmpty(rDtv_Data.RowFilter.Trim())))
                {
                    rDtv_Data.RowFilter = rDtv_Data.RowFilter + " AND (" + vStr_WhereClause + ")";
                }
                else
                {
                    rDtv_Data.RowFilter = "(" + vStr_WhereClause + ")";
                }
            }

            for (lInt_Loop = 0; lInt_Loop <= rDtv_Data.Count - 1; lInt_Loop++)
            {
                lCol_Rows.Add(rDtv_Data[lInt_Loop].Row);
            }

            if (((vStr_WhereClause != null)))
            {
                rDtv_Data.RowFilter = lStr_FiltroOriginal;
            }

            lArr_Rows = (DataRow[]) lCol_Rows.ToArray(typeof(DataRow));

            Update(ref lArr_Rows, vStr_ColumnName, vObj_ColumnValue);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Altera o valor de uma coluna informada dentre as linhas para um certo valor informado
        /// </summary>
        /// <param name="rArr_Data"></param>
        /// <param name="vStr_ColumnName"></param>
        /// <param name="vObj_ColumnValue"></param>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[limaot]	26/03/2007	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Update(ref DataRow[] rArr_Data, string vStr_ColumnName, object vObj_ColumnValue)
        {
            DataRow lDtr_Data = null;

            if ((rArr_Data == null))
            {
                throw new ApplicationException("Update recebeu um array de linhas inválido!");
            }
            if ((rArr_Data.Length > 0 && !rArr_Data[0].Table.Columns.Contains(vStr_ColumnName)))
            {
                throw new ApplicationException("Não foi encontrada uma coluna de nome \"" + vStr_ColumnName + "\" no array de linhas recebido!");
            }

            foreach (DataRow lDtr_Data_loopVariable in rArr_Data)
            {
                lDtr_Data = lDtr_Data_loopVariable;
                lDtr_Data[vStr_ColumnName] = vObj_ColumnValue;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Converte um array de linhas em um datatable
        /// </summary>
        /// <param name="vArr_Rows"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[limaot]	26/03/2007	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DataTable DataRowArrayToDataTable(DataRow[] vArr_Rows)
        {
            DataTable lDtt_Data = null;

            if ((vArr_Rows == null || vArr_Rows.Length == 0))
            {
                return new DataTable();
            }
            else
            {
                lDtt_Data = vArr_Rows[0].Table.Clone();
                DataUtil.AddRows(vArr_Rows, ref lDtt_Data);
                return lDtt_Data;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Copia dados da uma tabela de acordo com um filtro criando um novo Datatable
        /// </summary>
        /// <param name="vDtt_Source"></param>
        /// <param name="vStr_FilterCriteria"></param>
        /// <param name="vStr_ColumnNames"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[limaot]	26/03/2007	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DataTable CopyTable(DataTable vDtt_Source, string vStr_FilterCriteria = null, string vStr_ColumnNames = null)
        {
            DataTable lDtt_Destination = vDtt_Source.Clone();
            DataRow[] lDtt_SourceRows = null;
            System.Data.DataColumn lDtc_Column = null;
            bool lBln_DeleteColumn = false;
            int lInt_ColumnCount = 0;

            if ((vStr_FilterCriteria == null))
            {
                lDtt_SourceRows = vDtt_Source.Select();
            }
            else
            {
                lDtt_SourceRows = vDtt_Source.Select(vStr_FilterCriteria);
            }

            DataUtil.AddRows(lDtt_SourceRows, ref lDtt_Destination);

            if (((vStr_ColumnNames != null)))
            {
                string[] lArr_ColumnNames = vStr_ColumnNames.Split(';');
                string lStr_ColumnName = null;

                // inicia indice de coluna
                lInt_ColumnCount = 0;

                // percorre as colunas de destino

                while ((lInt_ColumnCount < lDtt_Destination.Columns.Count))
                {
                    // pega a coluna
                    lDtc_Column = lDtt_Destination.Columns[lInt_ColumnCount];

                    // ajusta flag para como padar apagar a coluna da table de destino
                    lBln_DeleteColumn = true;

                    // percorre as colunas informadas
                    foreach (string lStr_ColumnName_loopVariable in lArr_ColumnNames)
                    {
                        lStr_ColumnName = lStr_ColumnName_loopVariable;
                        // a coluna de destino esta entre as colunas informadas?
                        if ((lDtc_Column.ColumnName.Trim().ToUpper() == lStr_ColumnName.Trim().ToUpper()))
                        {
                            // nao apaga
                            lBln_DeleteColumn = false;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                        // a coluna de destino esta entre as colunas informadas?

                    }
                    // percorre as colunas informadas

                    // apaga coluna?
                    if ((lBln_DeleteColumn == true))
                    {
                        lDtt_Destination.Columns.Remove(lDtc_Column);
                    }
                    else
                    {
                        // proxima coluna
                        lInt_ColumnCount += 1;
                    }
                    // apaga coluna?

                }
                // percorre as colunas de destino

                // commita
                lDtt_Destination.AcceptChanges();

            }

            return lDtt_Destination;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Copia dados de uma view criando um datatable a partir do mesmo
        /// </summary>
        /// <param name="vDtv_Source"></param>
        /// <param name="vStr_FilterCriteria"></param>
        /// <param name="vStr_ColumnNames"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[limaot]	26/03/2007	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DataTable CopyTable(DataView vDtv_Source, string vStr_FilterCriteria = null, string vStr_ColumnNames = null)
        {
            DataTable lDtt_Source = null;

            lDtt_Source = vDtv_Source.Table.Clone();
            DataUtil.AddView(ref vDtv_Source, ref lDtt_Source);

            return DataUtil.CopyTable(lDtt_Source, vStr_FilterCriteria, vStr_ColumnNames);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Converte dataview informado para texto
        /// </summary>
        /// <param name="rDtv_View"></param>
        /// <param name="vCfg_Columns"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[limaot]	26/03/2007	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public static string ConvertToString(ref System.Data.DataView rDtv_View, ColumnsConfigToString vCfg_Columns, System.Int32[] lArr_SelectedRows)
        {
            string functionReturnValue = null;
            int lInt_Column = 0;
            int lInt_ColumnTotal = 0;
            int lInt_Row = 0;
            int lInt_RowTotal = 0;
            System.Text.StringBuilder lStb_Text = new System.Text.StringBuilder();
            System.Text.StringBuilder lStb_Row = new System.Text.StringBuilder();
            string lStr_Format = string.Empty;
            string lStr_Value = null;
            object lObj_Value = null;
            System.Data.DataRowView lDrv_Row = null;
            ColumnsConfigToString.ColumnConfig lCfg_Column = default(ColumnsConfigToString.ColumnConfig);
            bool lBln_FirstColumn = false;

            // padrao
            functionReturnValue = string.Empty;

            // so se tiver dados

            if (((rDtv_View == null) == false))
            {

                try
                {
                    // marca como primeira
                    lBln_FirstColumn = true;

                    // tem estrutura de configuracao de coluna?

                    if (((vCfg_Columns == null) == false))
                    {
                        // pega o total de colunas
                        lInt_ColumnTotal = vCfg_Columns.Count;

                        // primeiro coloca o header das colunas
                        // percorre os estilos das colunas

                        for (lInt_Column = 1; lInt_Column <= lInt_ColumnTotal; lInt_Column++)
                        {
                            // pega o configurador
                            lCfg_Column = vCfg_Columns.GetItemByOrder(lInt_Column);

                            // se não for a primeira então coloca o tab
                            if ((lBln_FirstColumn == false))
                            {
                                // coloca tab
                                lStb_Row.Append(ControlChars.Tab);
                            }
                            else
                            {
                                // a proxima ja não é a primeira
                                lBln_FirstColumn = false;
                            }

                            // copia linha
                            lStb_Row.Append(lCfg_Column.Alias);

                        }

                        // inclui no text
                        lStb_Text.Append(lStb_Row.ToString());

                        // determina o total de linhas que serão 
                        // convertidas para string

                        // foi passado um indice de linhas?
                        if (((lArr_SelectedRows == null) == false))
                        {
                            // total é o tamanho do array
                            lInt_RowTotal = lArr_SelectedRows.GetLength(0);
                        }
                        else
                        {
                            // então são todas as linhas da view informada
                            lInt_RowTotal = rDtv_View.Count;
                        }

                        // percorre todas as linhas da grid

                        for (lInt_Row = 0; lInt_Row <= lInt_RowTotal - 1; lInt_Row++)
                        {
                            // marca como primeira
                            lBln_FirstColumn = true;

                            // zera a linha
                            lStb_Row.Length = 0;

                            // pega a linha
                            // tem array de indice de linhas (linhas selecionadas)?
                            if (((lArr_SelectedRows == null) == false))
                            {
                                // posicao da linha esta dentro da posicao do array
                                lDrv_Row = rDtv_View[lArr_SelectedRows[lInt_Row]];
                            }
                            else
                            {
                                // linha determinada pelo contador
                                lDrv_Row = rDtv_View[lInt_Row];
                            }

                            // percorre os estilos das colunas

                            for (lInt_Column = 1; lInt_Column <= lInt_ColumnTotal; lInt_Column++)
                            {
                                // pega a coluna
                                lCfg_Column = vCfg_Columns.GetItemByOrder(lInt_Column);

                                // tem valor valido?

                                if ((DBNull.Value.Equals(lDrv_Row[lCfg_Column.Name]) == false))
                                {
                                    // pega valor
                                    lObj_Value = lDrv_Row[lCfg_Column.Name];

                                    // pega formatacao da coluna
                                    lStr_Format = lCfg_Column.Format;

                                    // tem formatacao?
                                    if ((lStr_Format.Length > 0))
                                    {
                                        // com formatacao
                                        lStr_Value = string.Format(lStr_Format, lObj_Value);
                                    }
                                    else
                                    {
                                        // sem formatacao
                                        lStr_Value = lObj_Value.ToString();
                                    }

                                    // tem quebra de linha?

                                    if ((lStr_Value.IndexOf(ControlChars.Lf) >= 0))
                                    {
                                        // tira a quebra e substitui por um espaço
                                        lStr_Value = lStr_Value.Replace(ControlChars.NewLine, " ");

                                    }
                                    // tem quebra de linha?


                                }
                                else
                                {
                                    // sem valor
                                    lStr_Value = string.Empty;

                                }

                                // se tiver quebra de linha então
                                // retira


                                // se não for a primeira então coloca o tab
                                if ((lBln_FirstColumn == false))
                                {
                                    // coloca tab
                                    lStb_Row.Append(ControlChars.Tab);
                                }
                                else
                                {
                                    // a proxima ja não é a primeira
                                    lBln_FirstColumn = false;
                                }

                                // copia para linha
                                lStb_Row.Append(lStr_Value);

                            }
                            // percorrendo colunas da linha

                            // quebra de linhas
                            lStb_Text.Append(ControlChars.NewLine);

                            // copia linha
                            lStb_Text.Append(lStb_Row.ToString());

                        }
                        // percorrendo as linhas da view

                        // retorna
                        return lStb_Text.ToString().Trim();

                    }


                }
                catch 
                {
                    // dispara
                    throw;

                }

            }
            return functionReturnValue;

        }

        /// <summary>
        /// Converte a linha informada em string
        /// </summary>
        /// <param name="rDtr_Row"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ConvertToString(ref System.Data.DataRow rDtr_Row)
        {
            string functionReturnValue = null;

            // padrao
            functionReturnValue = string.Empty;

            // tem dado?
            if (((rDtr_Row == null) == false))
            {
                var rArrRow = new System.Data.DataRow[] { rDtr_Row };
                return ConvertToString(ref rArrRow);
            }
            return functionReturnValue;
            // tem dado?

        }

        /// <summary>
        /// Converte a table informada em string
        /// </summary>
        /// <param name="rDtt_Table"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ConvertToString(ref System.Data.DataTable rDtt_Table)
        {
            string functionReturnValue = null;

            // padrao
            functionReturnValue = string.Empty;

            // tem dado?
            if (((rDtt_Table == null) == false))
            {
                DataRow[] rArrRow = rDtt_Table.Select();
                return ConvertToString(ref rArrRow);
            }
            return functionReturnValue;
            // tem dado?

        }

        /// <summary>
        /// Converte a view de dados informada em string
        /// </summary>
        /// <param name="rDtv_View"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ConvertToString(ref System.Data.DataView rDtv_View)
        {
            string functionReturnValue = null;

            // padrao
            functionReturnValue = string.Empty;

            // tem dado?
            if (((rDtv_View == null) == false))
            {
                DataTable rDttTable = rDtv_View.ToTable();
                return ConvertToString(ref rDttTable);
            }
            return functionReturnValue;
            // tem dado?

        }

        /// <summary>
        /// Converte o array de linhas informadas em string
        /// </summary>
        /// <param name="rArr_Row"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ConvertToString(ref System.Data.DataRow[] rArr_Row)
        {
            string functionReturnValue = null;
            DataUtil.ColumnsConfigToString lCfg_Columns = default(DataUtil.ColumnsConfigToString);
            DataUtil.ColumnsConfigToString.ColumnConfig lCfg_Column = default(DataUtil.ColumnsConfigToString.ColumnConfig);
            System.Data.DataColumn lDtc_Column = null;
            System.Data.DataTable lDtt_Row = null;

            // padrao
            functionReturnValue = string.Empty;

            // so se tiver linhas e um table ligada as linhas

            if (((rArr_Row == null) == false) && (rArr_Row.GetLength(0) > 0) && ((rArr_Row[0].Table == null) == false) && ((rArr_Row[0].Table.Columns == null) == false) && (rArr_Row[0].Table.Columns.Count > 0))
            {

                try
                {
                    // monta configurador de colunas
                    lCfg_Columns = new DataUtil.ColumnsConfigToString();

                    // monta a estrutura de configuracao das colunas
                    // percorrendo a estrutura 

                    foreach (DataColumn lDtc_Column_loopVariable in rArr_Row[0].Table.Columns)
                    {
                        lDtc_Column = lDtc_Column_loopVariable;
                        // cria coluna de configuracao
                        lCfg_Column = new DataUtil.ColumnsConfigToString.ColumnConfig(lDtc_Column.ColumnName, lDtc_Column.ColumnName, string.Empty);

                        // inclui na colecao
                        lCfg_Columns.Add(lCfg_Column);

                    }

                    // cria table baseado na estrutura das linhas
                    lDtt_Row = rArr_Row[0].Table.Clone();
                    // copia as linhas para o novo table
                    AddRows(rArr_Row, ref lDtt_Row);
                    // agora obtem a string destas linhas
                   // functionReturnValue = DataUtil.ConvertToString(ref lDtt_Row.DefaultView, lCfg_Columns, null);

                }
                catch (Exception Ex)
                {
                    // dispara
                    throw;


                }
                finally
                {
                }

            }
            return functionReturnValue;

        }
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        /// Recebe um datatable e um arrey de colunas que se deseja utilizar como critério para  
        /// faze um distinct no datatable 
        /// </summary> 
        /// <param name="vDtt_Table">Datatable que se deseja retornar as linhas unicas</param> 
        /// <param name="vArr_ColumnNames">Array contento o nome das colunas que serão utilizadas como critério do distinct</param> 
        /// <returns>Um array de datarow com as linhas unicas</returns> 
        /// <remarks> 
        /// </remarks> 
        /// <history> 
        ///         [santanfa]        23/12/2008        Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected DataRow[] GetDistinctRows(DataTable vDtt_Table, string[] vArr_ColumnNames)
        {
            DataRow lDtr_Row = null;
            Hashtable lCol_Hash = new Hashtable();
            string lStr_IdUnicoLinha = null;
            ArrayList lCol_Resultado = new ArrayList();

            if ((vDtt_Table == null))
            {
                throw new ArgumentNullException("vDtt_Table");
            }

            if ((vArr_ColumnNames == null))
            {
                throw new ArgumentNullException("vArr_ColumnNames");
            }

            //Para cada linha do DataTable
            foreach (DataRow lDtr_Row_loopVariable in vDtt_Table.Rows)
            {
                lDtr_Row = lDtr_Row_loopVariable;
                //Montar um id unico da linha a partir das colunas informadas
                lStr_IdUnicoLinha = this.GetUniqueRowId(lDtr_Row, vArr_ColumnNames);
                //Verifica se ja existe uma linha do datatable com o mesmo ID
                if ((!lCol_Hash.ContainsKey(lStr_IdUnicoLinha)))
                {
                    //Caso não exista, adiciona o datarow no array list com as linhas unicas
                    lCol_Resultado.Add(lDtr_Row);
                    //Adiciona o id no hash para não haver linhas duplicadas
                    lCol_Hash.Add(lStr_IdUnicoLinha, null);
                }
            }
            //Converte e retorna um array de datarow.
            return (DataRow[]) lCol_Resultado.ToArray(typeof(DataRow));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vDtr_Row"></param>
        /// <param name="vArr_ColumnNames"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetUniqueRowId(DataRow vDtr_Row, string[] vArr_ColumnNames)
        {
            string lStr_ColumnName = null;
            object lObj_ValorUnico = System.DBNull.Value;
            string lStr_IdUnico = string.Empty;

            //Monta uma string com o valor de todos os campos para que seja feito o distinct
            //Para cada coluna passada no array com os nomes das colunas

            foreach (string lStr_ColumnName_loopVariable in vArr_ColumnNames)
            {
                lStr_ColumnName = lStr_ColumnName_loopVariable;
                //Se existe coluna na linha com o mesmo nome da coluna corrente
                if ((vDtr_Row.Table.Columns.Contains(lStr_ColumnName)))
                {
                    //Ler o valor da coluna no datarow
                    lObj_ValorUnico = vDtr_Row[lStr_ColumnName];

                    //Se for nulo
                    if ((DBNull.Value.Equals(lObj_ValorUnico)))
                    {
                        //Atribuir NULL ao valor unico
                        lObj_ValorUnico = "NULL";
                    }

                    //Converte o valor da coluna para string
                    lObj_ValorUnico = Convert.ToString(lObj_ValorUnico);
                    //Concatena para formar o ID único da lina
                    lStr_IdUnico += lObj_ValorUnico + "-";

                }

            }

            //Se o tamanho do id unico da linha for maior que zero, ou seja, pelo menos um campo do datarow nao estive nulo
            if ((lStr_IdUnico.Length > 0))
            {
                //Remover o ultimo digito ("-")
                lStr_IdUnico = lStr_IdUnico.Remove(lStr_IdUnico.Length - 1, 1);
            }

            return lStr_IdUnico;

        }

        #region " Classes relacionadas (ColumnsConfigToString) "

        /// -----------------------------------------------------------------------------
        /// Project	 : .Framework.Common
        /// Class	 : Framework.Common.ColumnsConfigToString
        /// 
        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     Classe que guarda uma estrutura com a configuracao das colunas
        ///     de um datatable, dataview, grid, ... que pode ser utilizado
        ///     quando houver a necessidade de se converter este tipo de estrutura
        ///     para texto
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[limaot]	26/03/2007	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public class ColumnsConfigToString
        {

            // estrutura que guarda a coleção de colunas
            private System.Collections.Hashtable iHst_Columns;

            private System.Collections.SortedList iSls_Order;
            /// -----------------------------------------------------------------------------
            /// <summary>
            ///     Retorna o numero de colunas configuradas
            /// </summary>
            /// <value></value>
            /// <remarks>
            /// </remarks>
            /// <history>
            /// 	[limaot]	26/03/2007	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public int Count
            {
                get
                {
                    // hastable válido?
                    if (((this.iHst_Columns == null) == false))
                    {
                        return this.iHst_Columns.Count;
                    }
                    else
                    {
                        // nao tem nada
                        return 0;
                    }
                }
            }

            /// -----------------------------------------------------------------------------
            /// <summary>
            ///     Construtor
            /// </summary>
            /// <remarks>
            /// </remarks>
            /// <history>
            /// 	[limaot]	26/03/2007	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public ColumnsConfigToString()
            {
                // inicializa hash
                this.iHst_Columns = new System.Collections.Hashtable();
                // inicializa controle de ordem das colunas
                this.iSls_Order = new System.Collections.SortedList();
            }

            /// -----------------------------------------------------------------------------
            /// <summary>
            ///     Apaga todas as configuracoes de coluna existentes
            /// </summary>
            /// <remarks>
            /// </remarks>
            /// <history>
            /// 	[limaot]	26/03/2007	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public void Clear()
            {
                this.iHst_Columns.Clear();
                this.iSls_Order.Clear();
            }

            /// -----------------------------------------------------------------------------
            /// <summary>
            ///     Inclui a configuracao de coluna informada
            /// </summary>
            /// <param name="vCfg_Column"></param>
            /// <remarks>
            /// </remarks>
            /// <history>
            /// 	[limaot]	26/03/2007	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public void Add(ColumnsConfigToString.ColumnConfig vCfg_Column)
            {
                // inclui na estrutura que guarda a ordem
                this.iSls_Order.Add(this.iHst_Columns.Count + 1, vCfg_Column.IdName);
                // inclui no hashtable que guarda a configuracap
                this.iHst_Columns.Add(vCfg_Column.IdName, vCfg_Column);
            }

            /// -----------------------------------------------------------------------------
            /// <summary>
            ///     Obtem o item de nome informado
            /// </summary>
            /// <param name="vStr_Name"></param>
            /// <returns></returns>
            /// <remarks>
            /// </remarks>
            /// <history>
            /// 	[limaot]	26/03/2007	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public ColumnsConfigToString.ColumnConfig GetItemByName(string vStr_Name)
            {
                // retorna
                return (ColumnsConfigToString.ColumnConfig)this.iHst_Columns[vStr_Name];
            }

            /// -----------------------------------------------------------------------------
            /// <summary>
            ///     Obtem o item pela ordem
            /// </summary>
            /// <param name="vInt_Order"></param>
            /// <returns></returns>
            /// <remarks>
            /// </remarks>
            /// <history>
            /// 	[limaot]	26/03/2007	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public ColumnsConfigToString.ColumnConfig GetItemByOrder(int vInt_Order)
            {
                string lStr_Name = null;

                // obtem o nome
                lStr_Name = Convert.ToString(this.iSls_Order[vInt_Order]);

                // obtem o nome da coluna pela ordem desejada
                return this.GetItemByName(lStr_Name);

            }

            #region " Classe interna (ColumnConfig) "

            /// -----------------------------------------------------------------------------
            /// Project	 : .Framework.Common
            /// Class	 : Framework.Common.ColumnsConfigToString.ColumnConfig
            /// 
            /// -----------------------------------------------------------------------------
            /// <summary>
            ///     Classe que guarda as informacoes mantidas para cada coluna
            /// </summary>
            /// <remarks>
            /// </remarks>
            /// <history>
            /// 	[limaot]	26/03/2007	Created
            /// </history>
            /// -----------------------------------------------------------------------------
            public class ColumnConfig
            {

                // identificador
                private string iStr_IdName = string.Empty;
                // nome da coluna
                private string iStr_Name = string.Empty;
                // alias da coluna
                private string iStr_Alias = string.Empty;
                // formatacao da coluna

                private string iStr_Format = string.Empty;
                /// -----------------------------------------------------------------------------
                /// <summary>
                ///     Identificador unico
                /// </summary>
                /// <value></value>
                /// <remarks>
                /// </remarks>
                /// <history>
                /// 	[limaot]	26/03/2007	Created
                /// </history>
                /// -----------------------------------------------------------------------------
                public string IdName
                {
                    get { return this.iStr_IdName; }
                }

                /// -----------------------------------------------------------------------------
                /// <summary>
                ///     Nome da coluna
                /// </summary>
                /// <value></value>
                /// <remarks>
                /// </remarks>
                /// <history>
                /// 	[limaot]	26/03/2007	Created
                /// </history>
                /// -----------------------------------------------------------------------------
                public string Name
                {
                    get { return this.iStr_Name; }
                }

                /// -----------------------------------------------------------------------------
                /// <summary>
                ///     Alias da coluna
                /// </summary>
                /// <value></value>
                /// <remarks>
                /// </remarks>
                /// <history>
                /// 	[limaot]	26/03/2007	Created
                /// </history>
                /// -----------------------------------------------------------------------------
                public string Alias
                {
                    get { return this.iStr_Alias; }
                }

                /// -----------------------------------------------------------------------------
                /// <summary>
                ///     Formatacao da coluna
                /// </summary>
                /// <value></value>
                /// <remarks>
                /// </remarks>
                /// <history>
                /// 	[limaot]	26/03/2007	Created
                /// </history>
                /// -----------------------------------------------------------------------------
                public string Format
                {
                    get { return this.iStr_Format; }
                }

                /// -----------------------------------------------------------------------------
                /// <summary>
                ///     Construtor
                /// </summary>
                /// <param name="vStr_Name"></param>
                /// <remarks>
                /// </remarks>
                /// <history>
                /// 	[limaot]	26/03/2007	Created
                /// </history>
                /// -----------------------------------------------------------------------------
                public ColumnConfig(string vStr_Name)
                {
                    this.iStr_IdName = vStr_Name.Trim().ToUpper();
                    this.iStr_Name = vStr_Name;
                }

                /// -----------------------------------------------------------------------------
                /// <summary>
                ///     Construtor com passagem de nome, alias e formatacao
                /// </summary>
                /// <param name="vStr_Name"></param>
                /// <param name="vStr_Alias"></param>
                /// <param name="vStr_Format"></param>
                /// <remarks>
                /// </remarks>
                /// <history>
                /// 	[limaot]	26/03/2007	Created
                /// </history>
                /// -----------------------------------------------------------------------------
                // nome
                public ColumnConfig(string vStr_Name, string vStr_Alias, string vStr_Format)
                    : this(vStr_Name)
                {

                    // alias
                    this.iStr_Alias = vStr_Alias.Replace(ControlChars.NewLine, " ");

                    // formatacao
                    this.iStr_Format = vStr_Format;

                }

            }

            #endregion

        }

        #endregion

        #region "Exportação"

        public static void Export(DataRow[] vArr_Rows, string vStr_Filename, bool vBln_OpenFile = false)
		{
			string lStr_FileName = null;
			System.Text.StringBuilder lObj_FileContent = null;


			lStr_FileName = vStr_Filename;
			if ((System.IO.File.Exists(lStr_FileName))) {
				System.IO.File.Delete(lStr_FileName);
			}

			lObj_FileContent = DataRowsToCSV(vArr_Rows,false ,null , "=\"", "\"", "");

			ExportFile(lStr_FileName, lObj_FileContent);

			if ((vBln_OpenFile)) {
				OpenFile(lStr_FileName);
			}
		}

        public static void Export(DataRow[] vArr_Rows, string vStr_RelationName, string vStr_Filename, bool vBln_OpenFile = false)
		{
			string lStr_FileName = null;
			System.Text.StringBuilder lObj_FileContent = null;


			lStr_FileName = vStr_Filename;
			if ((System.IO.File.Exists(lStr_FileName))) {
				System.IO.File.Delete(lStr_FileName);
			}

			lObj_FileContent = DataRowsToCSV(vArr_Rows, false,null , "=\"", "\"", vStr_RelationName);

			ExportFile(lStr_FileName, lObj_FileContent);

			OpenFile(lStr_FileName);

		}

        private static System.Text.StringBuilder DataRowsToCSV(DataRow[] vArr_Rows, bool vBln_IncludeHeader = true, string vStr_Separator = Constants.Tab, string vStr_Prefix = "", string vStr_Sufix = "", string vStr_ChildRelationName = "", System.Text.StringBuilder rTxt_Output = null)
		{
			DataColumn lObj_Column = null;
			DataRow lDtr_Data = null;
			DataTable lDtt_Table = null;
			DataRow[] lArr_Linhas = null;
			int lInt_Pos = 0;
			int lInt_ColCount = 0;

			if ((rTxt_Output == null)) {
				rTxt_Output = new System.Text.StringBuilder();
			}

			if (((vArr_Rows != null) && vArr_Rows.Length > 0)) {
				lDtt_Table = vArr_Rows[0].Table;
				lInt_ColCount = lDtt_Table.Columns.Count;

				if ((vBln_IncludeHeader)) {
					for (lInt_Pos = 0; lInt_Pos <= lInt_ColCount - 1; lInt_Pos++) {
						lObj_Column = lDtt_Table.Columns[lInt_Pos];

						if (((lObj_Column.Caption != null) && !string.IsNullOrEmpty(lObj_Column.Caption))) {
							rTxt_Output.Append(vStr_Prefix + lObj_Column.Caption + vStr_Sufix + vStr_Separator);
						} else {
							rTxt_Output.Append(vStr_Prefix + lObj_Column.ColumnName + vStr_Sufix + vStr_Separator);
						}
					}
					rTxt_Output.Remove(rTxt_Output.Length - vStr_Separator.Length, vStr_Separator.Length);
				}

				rTxt_Output.Append(Constants.vbCrLf);

				foreach (DataRow lDtr_Data_loopVariable in vArr_Rows) {
					lDtr_Data = lDtr_Data_loopVariable;
					for (lInt_Pos = 0; lInt_Pos <= lInt_ColCount - 1; lInt_Pos++) {
						lObj_Column = lDtt_Table.Columns[lInt_Pos];

						if ((DBNull.Value.Equals(lDtr_Data[lObj_Column]))) {
							rTxt_Output.Append(vStr_Prefix + "" + vStr_Sufix + vStr_Separator);
						} else {
							if ((object.ReferenceEquals(lObj_Column.DataType, typeof(string)))) {
								rTxt_Output.Append(vStr_Prefix + Convert.ToString(lDtr_Data[lObj_Column]) + vStr_Sufix + vStr_Separator);
							} else {
								rTxt_Output.Append(Convert.ToString(lDtr_Data[lObj_Column]) + vStr_Separator);
							}
						}
					}

					rTxt_Output.Remove(rTxt_Output.Length - vStr_Separator.Length, vStr_Separator.Length);
					rTxt_Output.Append(Constants.vbCrLf);

					if ((!string.IsNullOrEmpty(vStr_ChildRelationName))) {
						lArr_Linhas = lDtr_Data.GetChildRows(vStr_ChildRelationName);
						DataRowsToCSV(lArr_Linhas, true, vStr_Separator, vStr_Prefix, vStr_Sufix,null , rTxt_Output);
					}
				}

				return rTxt_Output;
			} else {
				return rTxt_Output;
			}


		}

        private static void ExportFile(string vStr_FileName, System.Text.StringBuilder vObj_FileContent)
        {
            System.IO.StreamWriter lObj_Stream = null;

            if ((System.IO.File.Exists(vStr_FileName)))
            {
                System.IO.File.Delete(vStr_FileName);
            }

            lObj_Stream = new System.IO.StreamWriter(vStr_FileName, false, System.Text.Encoding.Unicode);
            try
            {
                lObj_Stream.Write(vObj_FileContent.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (((lObj_Stream != null)))
                {
                    lObj_Stream.Close();
                }
            }
        }

        private static void OpenFile(string vStr_NomeArquivo)
        {
            ProcessStartInfo lObj_StartInfo = new ProcessStartInfo(vStr_NomeArquivo);

            lObj_StartInfo.WindowStyle = ProcessWindowStyle.Normal;

            Process.Start(lObj_StartInfo);
        }

        #endregion

    }

}