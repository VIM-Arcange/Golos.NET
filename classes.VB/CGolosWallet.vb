Imports Newtonsoft.Json.Linq
Public Class CGolosWallet
    Inherits CGolosAPI

#Region "Constructors"
    Sub New(Optional strHostname As String = "127.0.0.1", Optional nPort As UShort = 8091)
        MyBase.New(strHostname, nPort)
    End Sub

#End Region

#Region "Public Methods"

    ''' Returns info such As client version, git version Of graphene/fc, version of boost, openssl.
    ''' Returns compile time info And client And dependencies versions
    Public Function about() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    ''' Cancel an order created With create_order
    ''' Parameters:
    '''    owner: The name Of the account owning the order To cancel_order (type String)
    '''    orderid: The unique identifier assigned To the order by its creator (type: uint32_t)
    '''    broadcast: true if you wish to broadcast the transaction (type: bool) 
    Public Function cancel_order(owner As String, orderid As UInt32, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(owner)
        arrParams.Add(orderid)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function challenge(challenger As String, challenged As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(challenger)
        arrParams.Add(challenged)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function change_recovery_account(owner As String, new_recovery_account As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(owner)
        arrParams.Add(new_recovery_account)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    ''' This method will convert GBG To GOLOS at the current_median_history price
    ''' one week from the time it Is executed. This method depends upon there being
    ''' a valid price feed.
    ''' Parameters:
    '''     from: The account requesting conversion Of its GBG i.e. "1.000 GBG"
    '''    (type: String)
    '''    amount: The amount Of GBG To convert (type: asset)
    '''    broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function convert_gbg(from As String, amount As Decimal, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(from)
        arrParams.Add(amount)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    ''' This method will genrate New owner, active, And memo keys For the New
    ''' account which will be controlable by this wallet. There Is a fee associated
    ''' With account creation that Is paid by the creator. The current account
    ''' creation fee can be found With the 'info' wallet command.
    ''' Parameters:
    '''    creator: The account creating the New account (type: String)
    '''    new_account_name: The name Of the New account (type: String)
    '''    json_meta: JSON Metadata associated With the New account (type: String)
    '''    broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function create_account(creator As String, new_account_name As String, json_meta As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(creator)
        arrParams.Add(new_account_name)
        arrParams.Add(json_meta)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''This method Is used by faucets To create New accounts For other users which
    '''must provide their desired keys. The resulting account may Not be
    '''controllable by this wallet. There Is a fee associated With account
    '''creation that Is paid by the creator. The current account creation fee can
    '''be found With the 'info' wallet command.
    '''Parameters:
    '''     creator: The account creating the New account (type: String)
    '''    newname: The name Of the New account (type: String)
    '''    json_meta: JSON Metadata associated With the New account (type: String)
    '''    owner: Public owner key Of the New account (type: public_key_type)
    '''    active: Public active key Of the New account (type: public_key_type)
    '''    posting: Public posting key Of the New account (type: public_key_type)
    '''    memo: Public memo key Of the New account (type: public_key_type)
    '''    broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function create_account_with_keys(creator As String, newname As String, json_meta As String, owner As String, active As String, posting As String, memo As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(creator)
        arrParams.Add(newname)
        arrParams.Add(json_meta)
        arrParams.Add(owner)
        arrParams.Add(active)
        arrParams.Add(posting)
        arrParams.Add(memo)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Creates a limit order at the price amount_to_sell / min_to_receive And will
    '''deduct amount_to_sell from account
    '''Parameters:
    '''     owner: The name Of the account creating the order (type: String)
    '''    order_id: Is a unique identifier assigned by the creator of the order,
    '''              it can be reused after the order has been filled (type: uint32_t)
    '''    amount_to_sell: The amount Of either GBG Or GOLOS you wish To sell (type: asset)
    '''    min_to_receive: The amount Of the other asset you will receive at a minimum (type: asset)
    '''    fill_or_kill: true if you want the order to be killed if it cannot immediately be filled (type: bool)
    '''    expiration: the time the order should expire If it has Not been filled (type: uint32_t)
    '''    broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function create_order(owner As String, order_id As UInt32, amount_to_sell As Decimal, min_to_receive As Decimal, fill_or_kill As Boolean, expiration As UInt32, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(owner)
        arrParams.Add(order_id)
        arrParams.Add(amount_to_sell)
        arrParams.Add(min_to_receive)
        arrParams.Add(fill_or_kill)
        arrParams.Add(expiration)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function decline_voting_rights(account As String, decline As Boolean, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account)
        arrParams.Add(decline)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function decrypt_memo(memo As String) As String
        Dim arrParams As New ArrayList
        arrParams.Add(memo)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams).ToString
    End Function


    '''Marks one account As following another account. Requires the posting authority Of the follower.
    '''
    '''Parameters:
    '''     what: - a set of things to follow: posts, comments, votes, ignore (type: Set<String>)
    Public Function follow(follower As String, following As String, what As ArrayList, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(follower)
        arrParams.Add(following)
        arrParams.Add(what)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Returns information about the given account.
    '''Parameters:
    '''     account_name: the name Of the account To provide information about (type: String)
    '''Returns
    '''    the Public account data stored In the blockchain
    Public Function get_account(account_name As String) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account_name)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Account operations have sequence numbers from 0 To N where N Is the most
    '''recent operation. This method returns operations In the range [from-limit,from]
    '''
    '''Parameters:
    '''     account: - account whose history will be returned (type: String)
    '''    from: - the absolute sequence number, -1 means most recent, limit Is
    '''    the number Of operations before from. (type: uint32_t)
    '''    limit: - the maximum number of items that can be queried (0 to 1000], must be less than from (type: uint32_t)
    Public Function get_account_history(account As String, from As UInt32, limit As UInt32) As JToken
        Dim arrParams As New ArrayList
        arrParams.Add(account)
        arrParams.Add(from)
        arrParams.Add(limit)
        Return call_api_token(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Returns the list Of witnesses producing blocks In the current round (21 Blocks)
    Public Function get_active_witnesses() As JArray
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    '''Returns the information about a block
    '''
    '''Parameters:
    '''num: Block num(type:   uint32_t)
    '''
    '''Returns
    '''    Public block data On the blockchain
    Public Function get_block(num As UInt32) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(num)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Returns conversion requests by an account
    '''
    '''Parameters:
    '''     owner: Account name Of the account owning the requests (type: String)
    '''
    '''Returns
    '''    All pending conversion requests by account
    Public Function get_conversion_requests(owner As String) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(owner)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_encrypted_memo(from As String, [to] As String, memo As String) As String
        Dim arrParams As New ArrayList
        arrParams.Add(from)
        arrParams.Add([to])
        arrParams.Add(memo)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams).ToString
    End Function


    '''Return the current price feed history
    Public Function get_feed_history() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    Public Function get_inbox(account As String, newest As DateTime, limit As UInt32) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(account)
        arrParams.Add(newest)
        arrParams.Add(limit)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Returns the queue Of pow miners waiting To produce blocks.
    Public Function get_miner_queue() As JArray
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    '''Gets the current order book For GOLOS:GBG
    '''
    '''Parameters:
    '''     limit: Maximum number Of orders To Return For bids And asks. Max Is 1000. (type: uint32_t)
    Public Function get_order_book(limit As UInt32) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(limit)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_outbox(account As String, newest As DateTime, limit As UInt32) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(account)
        arrParams.Add(newest)
        arrParams.Add(limit)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_owner_history(strAccount As String) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(strAccount)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_private_key(pubkey As String) As String
        Dim arrParams As New ArrayList
        arrParams.Add(pubkey)
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name, arrParams).ToString
    End Function

    ''' Get the WIF Private key corresponding To a Public key. The Private key must already be In the wallet.
    '''Parameters:
    '''role: - active | owner | posting | memo (type: String)
    Public Function get_private_key_from_password(account As String, role As String, password As String) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account)
        arrParams.Add(role)
        arrParams.Add(password)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    ''' Returns an uninitialized Object representing a given blockchain operation.
    '''
    '''This returns a Default-initialized Object Of the given type; it can be used
    '''during early development Of the wallet When we don't yet have custom
    '''commands for creating all of the operations the blockchain supports.
    '''
    '''Any operation the blockchain supports can be created Using the transaction
    '''builder's 'add_operation_to_builder_transaction()' , but to do that from
    '''the CLI you need To know what the JSON form Of the operation looks Like.
    '''This will give you a template you can fill In. It's better than nothing.
    '''
    '''Parameters:
    '''     operation_type: the type Of operation To Return, must be one Of the
    '''    operations defined In 'golos/chain/operations.hpp' (e.g., "global_parameters_update_operation") (type: String)
    '''
    '''Returns
    '''    a Default-constructed operation of the given type
    Public Function get_prototype_operation(operation_type As String) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(operation_type)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Returns the state info associated With the URL
    Public Function get_state(url As String) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(url)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Returns transaction by ID.
    Public Function get_transaction(trx_id As String) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(trx_id)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function get_withdraw_routes(account As String, type As String) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(account)
        arrParams.Add(type)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    Returns information about the given witness.
    '''
    '''    Parameters:
    '''        owner_account: the name Or id Of the witness account owner, Or the id of the witness (type: String)
    '''
    '''    Returns
    '''        the information about the witness stored In the block chain
    Public Function get_witness(owner_account As String) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(owner_account)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function


    '''    Returns detailed help On a Single API command.
    '''
    '''    Parameters:
    '''     method: the name Of the API command you want help With (type: Const String &)
    '''
    '''    Returns
    '''        a multi-line String suitable For displaying On a terminal
    Public Function gethelp(method As String) As String
        Dim arrParams As New ArrayList
        arrParams.Add(method)
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name, arrParams).ToString
    End Function

    '''Returns a list Of all commands supported by the wallet API.
    '''
    '''This lists Each command, along With its arguments And Return types. For
    '''more detailed help On a Single command, use 'get_help()'
    '''
    '''Returns
    '''    a multi-line String suitable For displaying On a terminal
    Public Function help() As String
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name).ToString
    End Function

    '''    Imports a WIF Private Key into the wallet To be used To sign transactionsby an account.
    '''
    '''    example: import_key 5KQwrPbwdL6PhXujxW37FSSQZ1JiwsST4cqQzDeyXtP79zkvFD3
    '''
    '''    Parameters:
    '''         wif_key: the WIF Private Key To import (type: String)
    Public Function import_key(wif_key As String) As Boolean
        Dim arrParams As New ArrayList
        arrParams.Add(wif_key)
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Returns info about the current state Of the blockchain
    Public Function info() As JObject
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    '''Checks whether the wallet Is locked (Is unable To use its Private keys).
    '''This state can be changed by calling 'lock()' or 'unlock()'.
    '''
    '''Returns
    '''    true if the wallet Is locked
    Public Function is_locked() As Boolean
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function


    '''Checks whether the wallet has just been created And has Not yet had a password set.
    '''Calling 'set_password' will transition the wallet to the locked state.
    '''
    '''Returns
    '''    true if the wallet Is New
    Public Function is_new() As Boolean
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    '''    Lists all accounts registered In the blockchain. This returns a list Of all
    '''    account names And their account ids, sorted by account name.
    '''
    '''    Use the 'lowerbound' and limit parameters to page through the list. To
    '''    retrieve all accounts, start by setting 'lowerbound' to the empty string
    '''    '""', and then each iteration, pass the last account name returned as the
    '''    'lowerbound' for the next 'list_accounts()' call.
    '''
    '''    Parameters:
    '''         lowerbound: the name Of the first account To Return. If the named
    '''        account does Not exist, the list will start at the account that
    '''        comes after 'lowerbound' (type: const string &)
    '''         limit: the maximum number Of accounts To Return (max: 1000) (type:
    '''        uint32_t)
    '''
    '''    Returns
    '''        a list Of accounts mapping account names To account ids
    Public Function list_accounts(lowerbound As String, limit As UInt32) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(lowerbound)
        arrParams.Add(limit)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function


    '''Dumps all Private keys owned by the wallet.
    '''
    '''The keys are printed In WIF format. You can import these keys into another
    '''wallet using 'import_key()'
    '''
    '''Returns
    '''    a map containing the Private keys, indexed by their Public key
    Public Function list_keys() As JToken
        Return call_api_token(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    '''Gets the account information For all accounts For which this wallet has aPrivate key
    Public Function list_my_accounts() As JArray
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    '''    Lists all witnesses registered In the blockchain. This returns a list Of
    '''    all account names that own witnesses, And the associated witness id, sorted
    '''    by name. This lists witnesses whether they are currently voted In Or Not.
    '''
    '''    Use the 'lowerbound' and limit parameters to page through the list. To
    '''    retrieve all witnesss, start by setting 'lowerbound' to the empty string
    '''    '""', and then each iteration, pass the last witness name returned as the
    '''    'lowerbound' for the next 'list_witnesss()' call.
    '''
    '''    Parameters:
    '''         lowerbound: the name Of the first witness To Return. If the named
    '''        witness does Not exist, the list will start at the witness that
    '''        comes after 'lowerbound' (type: const string &)
    '''         limit: the maximum number Of witnesss To Return (max: 1000) (type: uint32_t)
    '''
    '''    Returns
    '''        a list Of witnesss mapping witness names To witness ids
    '''
    Public Function list_witnesses(lowerbound As String, limit As UInt32) As JArray
        Dim arrParams As New ArrayList
        arrParams.Add(lowerbound)
        arrParams.Add(limit)
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    Loads a specified Graphene wallet.
    '''
    '''    The current wallet Is closed before the New wallet Is loaded.
    '''
    '''    Parameters:
    '''         wallet_filename: the filename Of the wallet JSON file To load. If
    '''        'wallet_filename' is empty, it reloads the existing wallet file (type String)
    '''
    '''    Returns
    '''        true if the specified wallet Is loaded
    Public Function load_wallet_file(wallet_filename As String) As Boolean
        Dim arrParams As New ArrayList
        arrParams.Add(wallet_filename)
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Locks the wallet immediately.
    Public Sub Lock()
        call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Sub

    Public Sub network_add_nodes(nodes As ArrayList)
        call_api(Reflection.MethodBase.GetCurrentMethod.Name)
    End Sub

    Public Function network_get_connected_peers() As JArray
        Return call_api_array(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    '''    Transforms a brain key To reduce the chance Of errors When re-entering the
    '''    key from memory.
    '''
    '''    This takes a user-supplied brain key And normalizes it into the form used
    '''    For generating private keys. In particular, this upper-cases all ASCII
    '''    characters And collapses multiple spaces into one.
    '''
    '''    Parameters:
    '''     key: the brain key As supplied by the user (type: String)
    '''
    '''    Returns
    '''        the brain key In its normalized form
    Public Function normalize_brain_key(key As String) As String
        Dim arrParams As New ArrayList
        arrParams.Add(key)
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name, arrParams).ToString
    End Function

    '''    Post Or update a comment.
    '''
    '''    Parameters:
    '''        author: the name Of the account authoring the comment (type: String)
    '''        permlink: the accountwide unique permlink For the comment (type
    '''        String)
    '''        parent_author: can be null If this Is a top level comment (type
    '''        String)
    '''        parent_permlink: becomes category If parent_author Is "" (type: String)
    '''        title: the title Of the comment (type: String)
    '''        body: the body Of the comment (type: String)
    '''        json: the json metadata Of the comment (type: String)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function post_comment(author As String, permlink As String, parent_author As String, parent_permlink As String, title As String, body As String, json As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(author)
        arrParams.Add(permlink)
        arrParams.Add(parent_author)
        arrParams.Add(parent_permlink)
        arrParams.Add(title)
        arrParams.Add(body)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function prove(challenged As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(challenged)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function


    '''    A witness can Public a price feed For the GOLOS:GBG market. The median
    '''    price feed Is used To process conversion requests from GBG To GOLOS.
    '''
    '''    Parameters:
    '''         witness: The witness publishing the price feed (type: String)
    '''        exchange_rate: The desired exchange rate (type: price)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function publish_feed(witness As String, exchange_rate As Decimal, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(witness)
        arrParams.Add(exchange_rate)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function recover_account(account_to_recover As String, recent_authority As Hashtable, new_authority As Hashtable, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account_to_recover)
        arrParams.Add(recent_authority)
        arrParams.Add(new_authority)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function request_account_recovery(recovery_account As String, account_to_recover As String, new_authority As Hashtable, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(recovery_account)
        arrParams.Add(account_to_recover)
        arrParams.Add(new_authority)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    Saves the current wallet To the given filename.
    '''
    '''    Parameters:
    '''wallet_filename: the filename Of the New wallet JSON file To create Or
    '''        overwrite. If 'wallet_filename' is empty, save to the current
    '''        filename. (type: String)
    Public Sub save_wallet_file(wallet_filename As String)
        Dim arrParams As New ArrayList
        arrParams.Add(wallet_filename)
        call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Sub

    Public Function send_private_message(from As String, [to] As String, subject As String, body As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(from)
        arrParams.Add([to])
        arrParams.Add(subject)
        arrParams.Add(body)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Sets a New password On the wallet.
    '''The wallet must be either 'new' or 'unlocked' to execute this command.
    Public Sub set_password(password As String)
        Dim arrParams As New ArrayList
        arrParams.Add(password)
        call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Sub

    Public Sub set_transaction_expiration(seconds As UInt32)
        Dim arrParams As New ArrayList
        arrParams.Add(seconds)
        call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Sub

    '''    Set the voting proxy For an account.
    '''
    '''    If a user does Not wish To take an active part In voting, they can choose
    '''    to allow another account to vote their stake.
    '''
    '''    Setting a vote proxy does Not remove your previous votes from the
    '''    blockchain, they remain there but are ignored. If you later null out your
    '''    vote proxy, your previous votes will take effect again.
    '''
    '''    This setting can be changed at any time.
    '''
    '''    Parameters:
    '''         account_to_modify: the name Or id Of the account To update (type
    '''        String)
    '''        proxy: the name Of account that should proxy To, Or empty String To
    '''        have no proxy (type: String)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function set_voting_proxy(account_to_modify As String, proxy As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account_to_modify)
        arrParams.Add(proxy)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function


    Public Function set_withdraw_vesting_route(from As String, [To] As String, percent As UInt16, auto_vest As Boolean, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(from)
        arrParams.Add([To])
        arrParams.Add(percent)
        arrParams.Add(auto_vest)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''Suggests a safe brain key To use For creating your account.
    ''''create_account_with_brain_key()' requires you to specify a 'brain key', a
    '''Long passphrase that provides enough entropy to generate cyrptographic
    '''keys. This function will suggest a suitably random string that should be
    '''easy to write down (And, with effort, memorize).

    '''Returns
    '''    a suggested brain_key
    Public Function suggest_brain_key() As String
        Return call_api_value(Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    '''    Transfer funds from one account To another. GOLOS And GBG can be
    '''    transferred.
    '''
    '''    Parameters:
    '''         from: The account the funds are coming from (type: String)
    '''        to: The account the funds are going To (type: String)
    '''        amount: The funds being transferred. i.e. "100.000 GOLOS" (type: asset)
    '''        memo: A memo For the transactionm, encrypted With the To account's
    '''        Public memo key (type: String)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function transfer(from As String, [to] As String, amount As Decimal, memo As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(from)
        arrParams.Add([to])
        arrParams.Add(amount)
        arrParams.Add(memo)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    Transfer GOLOS into a vesting fund represented by vesting shares (VESTS).
    '''    VESTS are required To vesting For a minimum Of one coin year And can be
    '''    withdrawn once a week over a two year withdraw period. VESTS are Protected
    '''    against dilution up until 90% Of GOLOS Is vesting.
    '''
    '''    Parameters:
    '''         from: The account the GOLOS Is coming from (type: String)
    '''        to: The account getting the VESTS (type: String)
    '''        amount: The amount Of GOLOS To vest i.e. "100.00 GOLOS" (type: asset)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function transfer_to_vesting(from As String, [to] As String, amount As Decimal, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(from)
        arrParams.Add([to])
        arrParams.Add(amount)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    ''' Transfers into savings happen immediately, transfers from savings take 72 hours
    '''
    Public Function transfer_to_savings(from As String, [To] As String, amount As String, memo As String, Optional broadcast As Boolean = False) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(from)
        arrParams.Add([To])
        arrParams.Add(amount)
        arrParams.Add(memo)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    ''' @param request_id - an unique ID assigned by from account, the id Is used to cancel the operation And can be reused after the transfer completes
    '''
    Public Function transfer_from_savings(from As String, request_id As UInt32, [To] As String, amount As String, memo As String, Optional broadcast As Boolean = False) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(from)
        arrParams.Add(request_id)
        arrParams.Add([To])
        arrParams.Add(amount)
        arrParams.Add(memo)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    ''' @param request_id the id used in transfer_from_savings
    ''' @param from the account that initiated the transfer
    '''
    Public Function cancel_transfer_from_savings(from As String, request_id As UInt32, Optional broadcast As Boolean = False) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(from)
        arrParams.Add(request_id)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    The wallet remain unlocked until the 'lock' is called or the program exits.
    '''
    '''    Parameters:
    '''         password: the password previously Set With 'set_password()' (type: String)
    Public Sub unlock(password As String)
        Dim arrParams As New ArrayList
        arrParams.Add(password)
        call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Sub

    '''    This method updates the keys Of an existing account.
    '''
    '''    Parameters:
    '''         accountname: The name Of the account (type: String)
    '''        json_meta: New JSON Metadata to be associated with the account (type
    '''        String)
    '''        owner: New public owner key for the account (type: public_key_type)
    '''        active: New public active key for the account (type: public_key_type)
    '''        posting: New public posting key for the account (type: public_key_type)
    '''        memo: New public memo key for the account (type: public_key_type)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function update_account(accountname As String, json_meta As String, owner As String, active As String, posting As String, memo As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(accountname)
        arrParams.Add(json_meta)
        arrParams.Add(owner)
        arrParams.Add(active)
        arrParams.Add(memo)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    update_account_auth_key(string account_name, authority_type type, public_key_type key, weight_type weight, bool broadcast)
    '''
    '''    This method updates the key Of an authority For an exisiting account.
    '''    Warning: You can create impossible authorities Using this method. The
    '''    method will fail If you create an impossible owner authority, but will
    '''    allow impossible active And posting authorities.
    '''
    '''    Parameters:
    '''         account_name: The name Of the account whose authority you wish To
    '''        update (type: String)
    '''        type: The authority type. e.g. owner, active, Or posting (type:
    '''        authority_type)
    '''        key: The Public key To add To the authority (type: public_key_type)
    '''        weight: The weight the key should have In the authority. A weight Of 0
    '''        indicates the removal Of the key. (type: weight_type)
    '''        broadcast: true if you wish to broadcast the transaction. (type: bool)
    Public Function update_account_auth_account(account_name As String, type As String, auth_account As String, weight As UInt16, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account_name)
        arrParams.Add(type)
        arrParams.Add(auth_account)
        arrParams.Add(weight)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    Public Function update_account_auth_key(account_name As String, type As String, key As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account_name)
        arrParams.Add(type)
        arrParams.Add(key)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    This method updates the weight threshold Of an authority For an account.
    '''    Warning: You can create impossible authorities Using this method As well As
    '''    implicitly met authorities. The method will fail If you create an
    '''    implicitly true authority And if you create an impossible owner authoroty,
    '''    but will allow impossible active And posting authorities.
    '''
    '''    Parameters:
    '''         account_name: The name Of the account whose authority you wish To
    '''        update (type: String)
    '''        type: The authority type. e.g. owner, active, Or posting (type:
    '''        authority_type)
    '''        threshold: The weight threshold required For the authority To be met
    '''        (type: uint32_t)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function update_account_auth_threshold(account_name As String, type As String, threshold As UInt32, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account_name)
        arrParams.Add(type)
        arrParams.Add(threshold)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    This method updates the memo key Of an account
    '''
    '''    Parameters:
    '''        account_name: The name Of the account you wish To update (type: String)
    '''        key: The New memo public key (type: public_key_type)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function update_account_memo_key(account_name As String, key As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account_name)
        arrParams.Add(key)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    This method updates the account JSON metadata
    '''
    '''    Parameters:
    '''         account_name: The name Of the account you wish To update (type: String)
    '''        json_meta: The New JSON metadata for the account. This overrides
    '''        existing metadata(Type: String)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function update_account_meta(account_name As String, json_meta As String, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account_name)
        arrParams.Add(json_meta)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function


    ''' Update a witness Object owned by the given account.
    '''
    ''' Parameters:
    '''     witness_name:       The name Of the witness's owner account. Also accepts the ID of the owner account Or the ID of the witness. (type: String)
    '''     url:                Same as for create_witness. The empty string makes it remain the same. (type: String)
    '''     block_signing_key:  The New block signing public key. The empty string makes it remain the same. (type: public_key_type)
    '''     props:              The chain properties the witness Is voting On. (type: Const chain_properties &)
    '''     broadcast:          true if you wish to broadcast the transaction. (type: bool)
    Public Function update_witness(witness_name As String, url As String, block_signing_key As String, props As JArray, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(witness_name)
        arrParams.Add(url)
        arrParams.Add(block_signing_key)
        arrParams.Add(props)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    Vote on a comment to be paid GOLOS
    '''
    '''    Parameters:
    '''         voter: The account voting (type: String)
    '''        author: The author Of the comment To be voted On (type: String)
    '''        permlink: The permlink Of the comment To be voted On. (author,
    '''        permlink) Is a unique pair (type: String)
    '''        weight: The weight [-100,100] Of the vote (type: int16_t)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function vote(voter As String, author As String, permlink As String, weight As Int16, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(voter)
        arrParams.Add(author)
        arrParams.Add(permlink)
        arrParams.Add(weight)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function


    '''    Vote for a witness to become a block producer. By default an account has
    '''    Not voted positively Or negatively for a witness. The account can either
    '''    vote for with positively votes Or against with negative votes. The vote
    '''    will remain until updated With another vote. Vote strength Is determined by
    '''    the accounts vesting shares.
    '''
    '''    Parameters:
    '''         account_to_vote_with: The account voting For a witness (type: String)
    '''        witness_to_vote_for: The witness that Is being voted For (type: String)
    '''        approve: true if the account Is voting for the account to be able to be
    '''        a block produce (type: bool)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function vote_for_witness(account_to_vote_with As String, witness_to_vote_for As String, approve As Boolean, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(account_to_vote_with)
        arrParams.Add(witness_to_vote_for)
        arrParams.Add(approve)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

    '''    Set up a vesting withdraw request. The request Is fulfilled once a week
    '''    over the Next two year (104 weeks).
    '''
    '''    Parameters:
    '''         from: The account the VESTS are withdrawn from (type: String)
    '''        vesting_shares: The amount Of VESTS To withdraw over the Next two
    '''        years. Each week (amount/104) shares are withdrawn And depositted
    '''        back as GOLOS. i.e. "10.000000 VESTS" (type: asset)
    '''        broadcast: true if you wish to broadcast the transaction (type: bool)
    Public Function withdraw_vesting(from As String, vesting_shares As Decimal, Optional broadcast As Boolean = True) As JObject
        Dim arrParams As New ArrayList
        arrParams.Add(from)
        arrParams.Add(vesting_shares)
        arrParams.Add(broadcast)
        Return call_api(Reflection.MethodBase.GetCurrentMethod.Name, arrParams)
    End Function

#End Region

End Class

