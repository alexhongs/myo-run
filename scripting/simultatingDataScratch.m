%% set up file io

fidData = fopen( 'simulatedData.csv', 'w' );
fidLabels = fopen( 'simulatedLabels.csv', 'w' );

%% extension
time=[1:1:300]; %in seconds
snr=10;

for trial = 1:10
    
    snr=5+(15-5)*rand(1)
    %snr=10

    e1=abs(int32(awgn(60*sech(0.4*(time-100)),snr,'measured')));
    e2=abs(int32(awgn(60*sech(0.4*(time-100)),snr,'measured')));
    e3=abs(int32(awgn(350*sech(0.4*(time-120)),snr,'measured')));
    e4=abs(int32(awgn(0*time,snr,'measured')));
    e5=abs(int32(awgn(400*sech(0.4*(time-100)),snr,'measured')));
    
    for sample = 1:300
        fprintf( fidData, '[%d, %d, %d, %d, %d]; ',e1(sample),e2(sample),e3(sample),e4(sample),e5(sample));
    end
    
    fprintf( fidData, '\n');
    fprintf( fidLabels, 'e\n');

end

%blue, red, orange, purple, green

%% plot waveforms

plot(time,e1);
hold on
plot(time,e2);
plot(time,e3);
plot(time,e4);
plot(time,e5);
legend('e1','e2','e3','e4','e5');
hold off

