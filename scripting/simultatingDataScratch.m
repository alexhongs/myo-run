%% set up file io

fidData = fopen( 'simulatedData.csv', 'w' );
fidLabels = fopen( 'simulatedLabels.csv', 'w' );
numTrialsPerLabel=200

%% extension
time=[1:1:300]; %in seconds
snr=10;

for trial = 1:numTrialsPerLabel
    
    snr=2+(15-2)*rand(1);
    %snr=10
    noiseScale=10;
    n1=rand(1)*noiseScale;
    n2=rand(1)*noiseScale;
    %n3=rand(1)*noiseScale;
    n3=max(rand(1),0.7);
    n4=rand(1)*noiseScale;
    %n5=rand(1)*noiseScale;
    n5=max(rand(1),0.7);
    

    e1=min(abs(int32(awgn(60*n1*sech(0.4*(time-100)),snr,'measured'))),1024);
    e2=min(abs(int32(awgn(60*n2*sech(0.4*(time-100)),snr,'measured'))),1024);
    e3=min(abs(int32(awgn(350*n3*sech(0.4*(time-120)),snr,'measured'))),1024);
    e4=min(abs(int32(awgn(0*n4*time,snr,'measured'))),1024);
    e5=min(abs(int32(awgn(400*n5*sech(0.4*(time-100)),snr,'measured'))),1024);
    
    for sample = 1:300
        fprintf( fidData, '[%d, %d, %d, %d, %d]; ',e1(sample),e2(sample),e3(sample),e4(sample),e5(sample));
    end
    
    fprintf( fidData, '\n');
    fprintf( fidLabels, 'e\n');

end

%blue, red, orange, purple, green

%% plot waveforms

% plot(time,e1);
% hold on
% plot(time,e2);
% plot(time,e3);
% plot(time,e4);
% plot(time,e5);
% legend('e1','e2','e3','e4','e5');
% hold off


%% flexation
%time=[1:1:300]; %in seconds

for trial = 1:numTrialsPerLabel
    
    snr=2+(15-2)*rand(1);
    %snr=10
    n1=max(rand(1),0.7);
    n2=max(rand(1),0.7);
    %n3=rand(1)*noiseScale;
    n3=max(rand(1),0.7);
    n4=max(rand(1),0.7);
    %n5=rand(1)*noiseScale;
    n5=max(rand(1),0.7);

    f1=min(abs(int32(awgn(n1*350*sech(0.4*(time-100)),snr,'measured'))),1024);
    f2=min(abs(int32(awgn(n2*350*sech(0.4*(time-100)),snr,'measured'))),1024);
    f3=min(abs(int32(awgn(n3*350*sech(0.4*(time-100)),snr,'measured'))),1024);
    f4=min(abs(int32(awgn(n4*350*sech(0.4*(time-100)),snr,'measured'))),1024);
    f5=min(abs(int32(awgn(n5*400*sech(0.4*(time-100)),snr,'measured'))),1024);
    
    for sample = 1:300
        fprintf( fidData, '[%d, %d, %d, %d, %d]; ',f1(sample),f2(sample),f3(sample),f4(sample),f5(sample));
    end
    
    fprintf( fidData, '\n');
    fprintf( fidLabels, 'f\n');

end

%% plot waveforms

% plot(time,f1);
% hold on
% plot(time,f2);
% plot(time,f3);
% plot(time,f4);
% plot(time,f5);
% legend('f1','f2','f3','f4','f5');
% hold off


%% pronation
%time=[1:1:300]; %in seconds

for trial = 1:numTrialsPerLabel
    
    snr=2+(15-2)*rand(1);
    
    noiseScale=10;
    n1=rand(1)*noiseScale;
    n2=max(rand(1),0.7);
    %n3=rand(1)*noiseScale;
    n3=max(rand(1),0.7);
    n4=rand(1)*noiseScale;
    %n5=rand(1)*noiseScale;
    n5=rand(1)*noiseScale;
    

    p1=min(abs(int32(awgn(n1*60*sech(0.4*(time-100)),snr,'measured'))),1024);
    p2=min(abs(int32(awgn(n2*200*sech(0.4*(time-100)),snr,'measured'))),1024);
    p3=min(abs(int32(awgn(n3*400*sech(0.4*(time-100)),snr,'measured'))),1024);
    p4=min(abs(int32(awgn(n4*10*sech(0.4*(time-100)),snr,'measured'))),1024);
    p5=min(abs(int32(awgn(n5*10*sech(0.4*(time-100)),snr,'measured'))),1024);
    
    for sample = 1:300
        fprintf( fidData, '[%d, %d, %d, %d, %d]; ',p1(sample),p2(sample),p3(sample),p4(sample),p5(sample));
    end
    
    fprintf( fidData, '\n');
    fprintf( fidLabels, 'p\n');

end

%% plot waveforms

% plot(time,p1);
% hold on
% plot(time,p2);
% plot(time,p3);
% plot(time,p4);
% plot(time,p5);
% legend('p1','p2','p3','p4','p5');
% hold off



%% supination
%time=[1:1:300]; %in seconds

%blue, red, orange, purple, green

for trial = 1:numTrialsPerLabel
    
    snr=2+(15-2)*rand(1);
    
    noiseScale=10;
    n1=max(rand(1),0.5);
    n2=rand(1)*noiseScale;
    %n3=rand(1)*noiseScale;
    n3=max(rand(1),0.5);
    n4=rand(1)*noiseScale;
    %n5=rand(1)*noiseScale;
    n5=max(rand(1),0.5);

    s1=min(abs(int32(awgn(n1*100*sech(0.4*(time-100)),snr,'measured'))),1024);
    s2=min(abs(int32(awgn(n2*10*sech(0.4*(time-100)),snr,'measured'))),1024);
    s3=min(abs(int32(awgn(n3*600*sech(0.4*(time-100)),snr,'measured'))),1024);
    s4=min(abs(int32(awgn(n4*10*sech(0.4*(time-100)),snr,'measured'))),1024);
    s5=min(abs(int32(awgn(n5*200*sech(0.4*(time-100)),snr,'measured'))),1024);
    
    for sample = 1:300
        fprintf( fidData, '[%d, %d, %d, %d, %d]; ',s1(sample),s2(sample),s3(sample),s4(sample),s5(sample));
    end
    
    fprintf( fidData, '\n');
    fprintf( fidLabels, 's\n');

end

%% plot waveforms
% 
% plot(time,s1);
% hold on
% plot(time,s2);
% plot(time,s3);
% plot(time,s4);
% plot(time,s5);
% legend('s1','s2','s3','s4','s5');
% hold off



%% relaxation

for trial = 1:numTrialsPerLabel
    
    snr=2+(15-2)*rand(1);
    %snr=100

    r1=abs(int32(awgn(10+0*time,snr,'measured')));
    r2=abs(int32(awgn(10+0*time,snr,'measured')));
    r3=abs(int32(awgn(10+0*time,snr,'measured')));
    r4=abs(int32(awgn(10+0*time,snr,'measured')));
    r5=abs(int32(awgn(10+0*time,snr,'measured')));
    
    for sample = 1:300
        fprintf( fidData, '[%d, %d, %d, %d, %d]; ',r1(sample),r2(sample),r3(sample),r4(sample),r5(sample));
    end
    
    fprintf( fidData, '\n');
    fprintf( fidLabels, 'r\n');

end

%% plot waveforms
% 
% plot(time,r1);
% hold on
% plot(time,r2);
% plot(time,r3);
% plot(time,r4);
% plot(time,r5);
% %legend('r1','r2','r3','r4','r5');
% hold off


%% close wrist
%time=[1:1:300]; %in seconds

for trial = 1:numTrialsPerLabel
    
    snr=2+(15-2)*rand(1);
    %snr=10
    n1=max(rand(1),0.7);
    n2=max(rand(1),0.7);
    %n3=rand(1)*noiseScale;
    n3=max(rand(1),0.7);
    n4=max(rand(1),0.7);
    %n5=rand(1)*noiseScale;
    n5=max(rand(1),0.7);

    c1=min(abs(int32(awgn(n1*200*sech(0.4*(time-100)),snr,'measured'))),1024);
    c2=min(abs(int32(awgn(min(n2*8000*sech(0.4*(time-100)),400),snr,'measured'))),1024);
    c5=min(abs(int32(awgn(min(n3*8000*sech(0.4*(time-100)),400),snr,'measured'))),1024);
    c4=min(abs(int32(awgn(n4*200*sech(0.4*(time-100)),snr,'measured'))),1024);
    c3=min(abs(int32(awgn(n5*200*sech(0.4*(time-100)),snr,'measured'))),1024);
    
    for sample = 1:300
        fprintf( fidData, '[%d, %d, %d, %d, %d]; ',c1(sample),c2(sample),c3(sample),c4(sample),c5(sample));
    end
    
    fprintf( fidData, '\n');
    fprintf( fidLabels, 'c\n');

end


%% plot waveforms
% 
% plot(time,c1);
% hold on
% plot(time,c2);
% plot(time,c3);
% plot(time,c4);
% plot(time,c5);
% legend('c1','c2','c3','c4','c5');
% hold off
