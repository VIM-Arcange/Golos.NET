Imports Newtonsoft.Json.Linq

Public Class CGolosd
    Inherits CGolosAPI

#Region "Constructors"
    Sub New(Optional strHostname As String = "127.0.0.1", Optional nPort As UShort = 8090)
        MyBase.New(strHostname, nPort)
    End Sub

    Sub New(strURI As String)
        MyBase.New(strURI)
    End Sub

#End Region

#Region "Public Methods"

    Public Function get_config() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    Public Function get_dynamic_global_properties() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    Public Function get_chain_properties() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    Public Function get_current_median_history_price() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    Public Function get_feed_history() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    Public Function get_witness_schedule() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    Public Function get_hardfork_version() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    Public Function get_next_scheduled_hardfork() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    Public Function get_accounts(arrAccounts As ArrayList) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(arrAccounts)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function lookup_account_names(arrAccounts As ArrayList) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(arrAccounts)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function lookup_accounts(strLowerbound As String, nLimit As UInteger) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(strLowerbound)
        arrParams.Add(nLimit)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_account_count() As JValue
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    Public Function get_owner_history(strAccount As String) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(strAccount)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_recovery_request(strAccount As String) As JObject
        Dim arrParams As New ArrayList()
        arrParams.Add(strAccount)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_block_header(lBlockID As Long) As JObject
        Dim arrParams As New ArrayList()
        arrParams.Add(lBlockID)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_block(lBlockID As Long) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(lBlockID)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_ops_in_block(block_num As Long, only_virtual As Boolean) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(block_num)
        arrParams.Add(only_virtual)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_witnesses(arrWitnesses As ArrayList) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(arrWitnesses)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_conversion_requests(strAccount As String) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(strAccount)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_witness_by_account(strAccount) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(strAccount)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_witnesses_by_vote(strFrom As String, nLimit As Integer) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(strFrom)
        arrParams.Add(nLimit)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_witness_count() As JValue
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    '''  if permlink Is "" then it will return all votes for author
    Public Function get_active_votes(author As String, permlink As String) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(author)
        arrParams.Add(permlink)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_content(strAuthor As String, strPermlink As String) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(strAuthor)
        arrParams.Add(strPermlink)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_content_replies(parent As String, parent_permlink As String) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(parent)
        arrParams.Add(parent_permlink)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    'vector<discussion> get_discussions_by_trending( const discussion_query& query )const;
    'vector<discussion> get_discussions_by_created( const discussion_query& query )const;
    'vector<discussion> get_discussions_by_active( const discussion_query& query )const;
    'vector<discussion> get_discussions_by_cashout( const discussion_query& query )const;
    'vector<discussion> get_discussions_by_payout( const discussion_query& query )const;
    'vector<discussion> get_discussions_by_votes( const discussion_query& query )const;
    'vector<discussion> get_discussions_by_children( const discussion_query& query )const;
    'vector<discussion> get_discussions_by_hot( const discussion_query& query )const;


    '''  Return the active discussions with the highest cumulative pending payouts without respect to category, total
    '''  pending payout means the pending payout of all children as well.
    Public Function get_replies_by_last_update(start_author As String, start_permlink As String, limit As UInteger) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(start_author)
        arrParams.Add(start_permlink)
        arrParams.Add(limit)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    ''' This method Is used to fetch all posts/comments by start_author that occur after before_date And start_permlink with up to limit being returned.
    '''
    ''' If start_permlink Is empty then only before_date will be considered. If both are specified the eariler to the two metrics will be used. This
    ''' should allow easy pagination.
    Public Function get_discussions_by_author_before_date(author As String, start_permlink As String, before_date As DateTime, limit As UInteger) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(author)
        arrParams.Add(start_permlink)
        arrParams.Add(before_date)
        arrParams.Add(limit)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    ''' Account operations have sequence numbers from 0 to N where N Is the most recent operation. This method
    ''' returns operations in the range [from-limit, from]
    '''
    ''' @param from - the absolute sequence number, -1 means most recent, limit Is the number of operations before from.
    ''' @param limit - the maximum number of items that can be queried (0 to 1000], must be less than from
    Public Function get_account_history(account As String, from As UInt64, limit As UInt32) As JToken
        Dim arrParams As New ArrayList
        arrParams.Add(account)
        arrParams.Add(from)
        arrParams.Add(limit)
        Return call_api_token(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

#End Region

End Class
