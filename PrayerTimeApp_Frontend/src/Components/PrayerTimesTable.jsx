function PrayerTimesTable({ prayerTimes }) {
  return (
    <table className="min-w-full text-sm text-left">
      <thead className="bg-gray-800 border-b">
        <tr>
          <th className="font-black px-6 py-3">#</th>
          <th className="px-6 py-3">Date</th>
          <th className="px-6 py-3">Day</th>
          <th className="px-6 py-3">Fajr</th>
          <th className="px-6 py-3">Syuruk</th>
          <th className="px-6 py-3">Dhuhr</th>
          <th className="px-6 py-3">Asr</th>
          <th className="px-6 py-3">Maghrib</th>
          <th className="px-6 py-3">Isha</th>
        </tr>
      </thead>
      <tbody>
        {prayerTimes.map((p, index) => (
          <tr
            key={index}
            className={index % 2 === 0 ? "bg-sky-800" : "bg-sky-600 border-b"}
          >
            <td className="px-6 py-4">{index + 1}</td>
            <td className="px-6 py-4">{p.date}</td>
            <td className="px-6 py-4">{p.day}</td>
            <td className="px-6 py-4">{p.fajr}</td>
            <td className="px-6 py-4">{p.syuruk}</td>
            <td className="px-6 py-4">{p.dhuhr}</td>
            <td className="px-6 py-4">{p.asr}</td>
            <td className="px-6 py-4">{p.maghrib}</td>
            <td className="px-6 py-4">{p.isha}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default PrayerTimesTable;
