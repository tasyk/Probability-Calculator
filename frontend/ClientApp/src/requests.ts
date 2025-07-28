import { API_BASE_URL } from './config'; 

export interface CalculateParams {
  a: string;
  b: string;
  type: string;
}

export interface CalculateResult {
  result?: string;
  error?: string;
}

export async function calculateProbability({ a, b, type }: CalculateParams): Promise<CalculateResult> {
  if (!API_BASE_URL) {
    return { error: "API URL is not defined. Please set REACT_APP_API_URL in your environment variables." };
  }

  const aNum = parseFloat(a);
  const bNum = parseFloat(b);    

  if (
    isNaN(aNum) ||
    isNaN(bNum) ||
    aNum < 0 ||
    aNum > 1 ||
    bNum < 0 ||
    bNum > 1
  ) {
    return { error: "Please enter valid probabilities between 0 and 1." };
  }

  try {
    const response = await fetch(`${API_BASE_URL}/api/calculator`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ a: aNum, b: bNum, type }),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData?.message || 'Error occurred');
    }

    const data = await response.json();
    return { result: data };
  } catch (err: any) {
    return { error: err.message || 'Error occurred' };
  }
}