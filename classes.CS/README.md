# Golos.NET

Microsoft C#.NET classes to interact with the Golos blockchain.

###Using Microsoft Visual Studio VB.NET classes
Create a VB.NET project and add following classes :
```
CJson.cs
CGolosAPI.cs
CGolosd.cs
CGolosWallet.cs
CWebSocket.cs
```

If you don't have Json.NET from Newtonsoft installed, simply add the provided Newtonsoft.Json.dll to you project references

That's it ! You're nowready to communicate with any Golos node, either local or remote 

## Remark
**CGolosd** class exposes all Golosd API (used to browse blockchain information)

**CGolosWallet** class exposes all cli_wallet API (used to interact with blockchain with posts, comments, votes, ...) 

## IMPORTANT NOTE

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
