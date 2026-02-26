import { FRIENDS_URL } from '$lib/config';
import { type CreateLovedoneRequest, type Hangout, type LovedOne } from '$lib/types/types';

const CREATED = 201;
const SUCCESS = 200;
const BAD_REQUEST = 400;
const NOT_FOUND = 404;

const LOVED_ONE_PATH = 'lovedone/';

export const createHangout = async (id: string) => {
  console.log(`adding hangout for lovedone: ${id}`);
  const date = formateDateToYYYYMMDDHHmmss(new Date());

  try {
    const response = await fetch(FRIENDS_URL + 'hangout', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ Date: date, LovedOneId: id })
    });
    if (response.status == BAD_REQUEST) {
      console.log(response.statusText + ': Bad Request - ID or Date no correct.');
      console.warn(`Date: ${date}`);
      console.warn(`ID: ${id}`);
    }
    if (response.status == CREATED) {
      const responseData: Hangout = await response.json();
      return responseData;
    }
  } catch (error) {
    console.error(error);
  }
};

export const createLovedOne = async (lovedOne: CreateLovedoneRequest) => {
  console.log(`Sending createLovedOne request...`)
  try {
    const response = await fetch(FRIENDS_URL + LOVED_ONE_PATH, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(lovedOne)
    });

    if (response.status == BAD_REQUEST) {
      console.log(response.statusText + ': Bad Request - Name or Relationship is not formatted correctly.');
      console.warn(`Name: ${lovedOne.name}`);
      console.warn(`Relationship: ${lovedOne.relationship}`)
    }

    if (response.status == CREATED) {
      const responseData: LovedOne = await response.json();
      return responseData;
    }

  } catch (error) {
    console.error((error));
  }
}

export const fetchLovedOne = async (id: string) => {
  console.log(`Fetching lovedone with id ${id}`);

  try {
    const response = await fetch(FRIENDS_URL + LOVED_ONE_PATH + id, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      }
    });

    if (response.status == BAD_REQUEST) {
      console.log(response.statusText + ': Bad Request - ID or Date no correct.');
    }

    if (response.status == NOT_FOUND) {
      console.log(`Lovedone with id: ${id} not found`);
    }

    if (response.status == SUCCESS) {
      console.log('Successfull fetch?');
      const responseData: LovedOne = await response.json();
      console.log(`Responsedata is what? : ${responseData}`);
      return responseData;
    }
  } catch (error) {
    console.error(error);
  }
};

const formateDateToYYYYMMDDHHmmss = (date: Date): string => {
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  const hours = String(date.getHours()).padStart(2, '0');
  const mins = String(date.getMinutes()).padStart(2, '0');
  const secs = String(date.getSeconds()).padStart(2, '0');

  return `${year}-${month}-${day}T${hours}:${mins}:${secs}`;
};
