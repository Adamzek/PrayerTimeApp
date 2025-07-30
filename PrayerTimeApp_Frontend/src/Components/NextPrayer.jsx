import React, { useState } from 'react';

function getNextPrayer(currentTime, prayerTimes) {
    if (!prayerTimes || prayerTimes.length === 0) return null;

    const today = prayerTimes.find(p => new Date(p.date).toDateString() === currentTime.toDateString());
    if (!today) return null;

    const keys = ['fajr', 'syuruk', 'dhuhr', 'asr', 'maghrib', 'isha'];
    for (const key of keys) {
        const [h, m] = today[key].split(':').map(Number);
        const prayerDate = new Date(currentTime);
        prayerDate.setHours(h, m, 0, 0);
        if (currentTime < prayerDate) {
            return { name: key.charAt(0).toUpperCase() + key.slice(1), time: today[key] };
        }
    }

    return { name: 'Fajr (next day)', time: prayerTimes[1]?.fajr || '—' };
}

function NextPrayer({ prayerTimes }) {
    const [currentTime] = useState(new Date());
    const nextPrayer = getNextPrayer(currentTime, prayerTimes);

    if (!nextPrayer) return null;

    return (
        <p className="text-white text-lg text-center mb-4">
            Next Prayer: {nextPrayer.name} at {nextPrayer.time}
        </p>
    );
}

export default NextPrayer;
