SELECT VideoName, COUNT(*) AS RecordCount
FROM Detections
GROUP BY VideoName;

SELECT VideoName, ClassName, COUNT(*) AS RecordCount
FROM Detections
GROUP BY VideoName, ClassName;