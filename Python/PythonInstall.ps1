$script = New-Object Net.WebClient

$script | Get-Member

$script.DownloadString("https://chocolatey.org/install.ps1")

iwr https://chocolatey.org/install.ps1 -UseBasicParsing | iex

choco upgrade chocolatey

choco install -y python3

python -V