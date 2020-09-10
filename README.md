# Golos.NET

.NET classes to interact with the Golos blockchain.

## Prerequisites

1. Golosd 

To communicate with **Golosd** you need either a local Golosd running node with  rpc-endpoint open or have access to a remote node using Websocket.

CGolosd as two constructors:

a. One  using a host and port. CGolosd object will try to connect to host **"127.0.0.1:8090"** by default

b. One using a websocket server URI

If you want tp use a local node, check [Golos Windows mining guide](https://Golosit.com/Golos/@bitcube/Golos-mining-in-microsoft-windows-a-miner-s-guide-part-2) for more information on how to do it.
You don't need to mine, a  syncing only node is OK

2. cli_wallet

To communicate with **cli-wallet** you need a local cli_wallet running node with rpc-endpoint open
By default, CGolosd object will try to connect to host **"127.0.0.1:8091"**

## Quick-Start

Golos.NET API is currently provided in different bundles :

1. Golos.NET **COM libraries** you can use in any application able to talk with COM objects (Words, Excel, ...)

2. Golos.NET **VB.NET classes** you can integrate in you own Microsoft Visual Studio project

3. Golos.NET **C#.NET classes** you can integrate in you own Microsoft Visual Studio project

## IMPORTANT NOTE

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
