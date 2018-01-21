FROM microsoft/iis
RUN powershell -executionpolicy bypass -command "add-windowsfeature Web-Asp-Net45"
RUN mkdir c:\install
ADD WebDeploy_2_10_amd64_en-US.msi /install/WebDeploy_2_10_amd64_en-US.msi
WORKDIR /install
RUN msiexec.exe /i c:\install\WebDeploy_2_10_amd64_en-US.msi /qn 
