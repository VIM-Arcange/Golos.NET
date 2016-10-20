Imports Newtonsoft.Json
Module Test

    Public Sub Main()

        Using oGolos As New CGolosd

            Debug.Print(oGolos.get_config.ToString)
            Debug.Print(oGolos.get_account_count.ToString)

            Dim arrAcct As New ArrayList
            arrAcct.Add("arcange")
            For Each oAccount In oGolos.get_accounts(arrAcct)
                Debug.Print(oAccount.ToString)
            Next

            Dim oBlock As Linq.JObject
            Dim oTrans As Linq.JObject

            Dim lLastBlockID As Long = oGolos.get_dynamic_global_properties().Item("last_irreversible_block_num")

            For lBlockID As Long = 1 To lLastBlockID
                oBlock = oGolos.get_block(lBlockID)
                If Not oBlock.Item("transactions") Is Nothing Then

                    Debug.Print(String.Format("Block {0} {1} contains {2} transactions", oBlock.Item("block_num"),
                                                                                         oBlock.Item("timestamp").ToObject(Of DateTime),
                                                                                         oBlock.Item("transactions").Count))

                    For Each oTrans In oBlock.Item("transactions")
                        Debug.Print(String.Format("Transaction is {0} operations", oTrans.Item("operations").Item(0).Item(0)))
                    Next
                End If
            Next

        End Using

    End Sub

End Module
