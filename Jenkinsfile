pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                sh 'echo "Hello Wangtp"'
                sh '''
                    echo $PWD
					docker images;
					docker ps -a;
                '''
            }
        }
    }
}