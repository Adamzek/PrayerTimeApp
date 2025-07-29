function ZoneSelector({ zones, selectedZone, onZoneChange }) {
    return (
        <select
            value={selectedZone}
            onChange={(e) => onZoneChange(e.target.value)}
            className="bg-white text-black border px-2 py-1 mb-4 rounded-md"
        >
            {zones.map((z, index) => (
                <option key={index} value={z.jakimCode}>
                    {z.negeri} - {z.daerah}
                </option>
            ))}
        </select>
    );
}

export default ZoneSelector;
