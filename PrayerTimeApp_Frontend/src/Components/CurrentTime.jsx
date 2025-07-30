import { useState, useEffect } from 'react';

function CurrentTime() {
    const [currentTime, setCurrentTime] = useState(new Date());

    useEffect(() => {
        const timer = setInterval(() => setCurrentTime(new Date()), 1000);
        return () => clearInterval(timer);
    }, []);

    return (
        <p className="text-white text-lg text-center mb-4">
            Current Time: {currentTime.toLocaleTimeString()}
        </p>
    );
}

export default CurrentTime;
