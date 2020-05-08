pipeline {
    agent any
    stages {
        stage('Build') {
            agent {
                docker{
                    image 'mcr.microsoft.com/dotnet/core/sdk:2.2'
                }
            }
            steps{
                sh 'dotnet publish -c Release dotnet_core.sln -o publish'
            }
        }
    }
}
